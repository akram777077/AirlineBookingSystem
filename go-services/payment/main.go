package main

import (
	"bytes"
	"encoding/json"
	"fmt"
	"io/ioutil"
	"log"
	"net/http"
	"os"

	"github.com/stripe/stripe-go/v72"
	"github.com/stripe/stripe-go/v72/paymentintent"
)

func main() {
	stripe.Key = os.Getenv("STRIPE_SECRET_KEY")

	http.HandleFunc("/create-payment-intent", handleCreatePaymentIntent)
	http.HandleFunc("/webhook", handleWebhook)

	port := os.Getenv("PORT")
	if port == "" {
		port = "8080"
	}

	log.Printf("Go Payment Service listening on port %s\n", port)
	log.Fatal(http.ListenAndServe(":"+port, nil))
}

func handleCreatePaymentIntent(w http.ResponseWriter, r *http.Request) {
	if r.Method != "POST" {
		http.Error(w, "Method not allowed", http.StatusMethodNotAllowed)
		return
	}

	var req struct {
		Amount   int64  `json:"amount"`
		Currency string `json:"currency"`
	} 

	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		http.Error(w, err.Error(), http.StatusBadRequest)
		return
	}

	params := &stripe.PaymentIntentParams{
		Amount:   stripe.Int64(req.Amount),
		Currency: stripe.String(req.Currency),
		AutomaticPaymentMethods: &stripe.PaymentIntentAutomaticPaymentMethodsParams{
			Enabled: stripe.Bool(true),
		},
	}

	pi, err := paymentintent.New(params)
	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		return
	}

	resp := struct {
		ClientSecret string `json:"clientSecret"`
	}{
		ClientSecret: pi.ClientSecret,
	}

	w.Header().Set("Content-Type", "application/json")
	json.NewEncoder(w).Encode(resp)
}

func handleWebhook(w http.ResponseWriter, r *http.Request) {
	const MaxBodyBytes = int64(65536)
	r.Body = http.MaxBytesReader(w, r.Body, MaxBodyBytes)
	payload, err := ioutil.ReadAll(r.Body)
	if err != nil {
		log.Printf("Error reading request body: %v\n", err)
		http.Error(w, "Error reading request body", http.StatusServiceUnavailable)
		return
	}

	endpointSecret := os.Getenv("STRIPE_WEBHOOK_SECRET")
	if endpointSecret == "" {
		log.Println("STRIPE_WEBHOOK_SECRET is not set in environment variables.")
		http.Error(w, "Server error: Webhook secret not configured", http.StatusInternalServerError)
		return
	}

	event, err := stripe.Webhook.ConstructEvent(payload, r.Header.Get("Stripe-Signature"), endpointSecret)
	if err != nil {
		log.Printf("Error verifying webhook signature: %v\n", err)
		http.Error(w, "Error verifying webhook signature", http.StatusBadRequest)
		return
	}

	// Handle the event
	sswitch event.Type {
	tcase "payment_intent.succeeded":
		var paymentIntent stripe.PaymentIntent
		err := json.Unmarshal(event.Data.Raw, &paymentIntent)
		if err != nil {
			log.Printf("Error unmarshaling event: %v\n", err)
			http.Error(w, "Error unmarshaling event", http.StatusInternalServerError)
			return
		}
		log.Printf("PaymentIntent succeeded: %s\n", paymentIntent.ID)

		// Call .NET backend to confirm payment
		dotnetBackendURL := os.Getenv("DOTNET_BACKEND_URL")
		if dotnetBackendURL == "" {
			log.Println("DOTNET_BACKEND_URL is not set in environment variables. Cannot confirm payment with backend.")
			w.WriteHeader(http.StatusOK) // Still return OK to Stripe
			return
		}

		// In a real application, you would get the BookingId from your database
		// based on the PaymentIntent ID or some other identifier stored during creation.
		// For this example, we'll use a placeholder.
		confirmationPayload := map[string]string{
			"bookingId":       "PLACEHOLDER_BOOKING_ID", // Replace with actual booking ID
			"paymentIntentId": paymentIntent.ID,
		}
		jsonPayload, _ := json.Marshal(confirmationPayload)

		resp, err := http.Post(fmt.Sprintf("%s/api/Payment/confirm-payment", dotnetBackendURL), "application/json", bytes.NewBuffer(jsonPayload))
		if err != nil {
			log.Printf("Error calling .NET backend for payment confirmation: %v\n", err)
			w.WriteHeader(http.StatusOK) // Still return OK to Stripe
			return
		}
		defer resp.Body.Close()

		if resp.StatusCode != http.StatusOK {
			bodyBytes, _ := ioutil.ReadAll(resp.Body)
			log.Printf("Error response from .NET backend (%d): %s\n", resp.StatusCode, string(bodyBytes))
		} else {
			log.Println("Successfully sent payment confirmation to .NET backend.")
		}
	case "payment_intent.payment_failed":
		var paymentIntent stripe.PaymentIntent
		err := json.Unmarshal(event.Data.Raw, &paymentIntent)
		if err != nil {
			log.Printf("Error unmarshaling event: %v\n", err)
			http.Error(w, "Error unmarshaling event", http.StatusInternalServerError)
			return
		}
		log.Printf("PaymentIntent failed: %s\n", paymentIntent.ID)
		// Further logic for failed payment
	default:
		log.Printf("Unhandled event type: %s\n", event.Type)
	}

	w.WriteHeader(http.StatusOK)
}
