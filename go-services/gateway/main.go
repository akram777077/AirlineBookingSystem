package main

import (
	"log"
	"net/http"
	"net/http/httputil"
	"net/url"
)

func main() {
	// The target URL of the downstream service (the dotnet API)
	target, err := url.Parse("http://localhost:5268")
	if err != nil {
		log.Fatalf("Failed to parse target URL: %v", err)
	}

	// Create a new reverse proxy
	proxy := httputil.NewSingleHostReverseProxy(target)

	// The handler for all incoming requests
	http.HandleFunc("/", func(w http.ResponseWriter, r *http.Request) {
		log.Printf("Forwarding request: %s %s", r.Method, r.URL.Path)
		// Update the request host to the target host
		r.Host = target.Host
		proxy.ServeHTTP(w, r)
	})

	// Start the gateway server
	log.Println("Starting API Gateway on :8080")
	if err := http.ListenAndServe(":8080", nil); err != nil {
		log.Fatalf("Failed to start API Gateway: %v", err)
	}
}
