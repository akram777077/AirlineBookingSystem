package main

import (
	"airline.com/booking/internal/database"
	"airline.com/booking/internal/models"
	"context"
	"encoding/json"
	"fmt"
	"github.com/google/uuid"
	"github.com/jackc/pgx/v5"
	"github.com/jackc/pgx/v5/pgxpool"
	"log"
	"net/http"
	"strings"
	"time"
)

type application struct {
	db *pgxpool.Pool
}

func main() {
	db, err := database.Connect()
	if err != nil {
		log.Fatalf("Could not connect to the database: %s\n", err)
	}
	defer db.Close()

	app := &application{
		db: db,
	}

	http.HandleFunc("/bookings/", app.bookingsRouter)

	fmt.Println("Booking service starting on port 8080...")
	if err := http.ListenAndServe(":8080", nil); err != nil {
		log.Fatalf("Could not start server: %s\n", err)
	}
}

func (app *application) bookingsRouter(w http.ResponseWriter, r *http.Request) {
	id := strings.TrimPrefix(r.URL.Path, "/bookings/")

	switch r.Method {
	case http.MethodGet:
		if id != "" {
			app.getBookingByID(w, r, id)
		} else {
			app.listBookings(w, r)
		}
	case http.MethodPost:
		if id != "" {
			jsonError(w, http.StatusMethodNotAllowed, "Cannot POST to a specific booking")
			return
		}
		app.createBookingHandler(w, r)
	case http.MethodPut:
		if id == "" {
			jsonError(w, http.StatusMethodNotAllowed, "PUT method requires a booking ID")
			return
		}
		app.updateBookingStatus(w, r, id)
	case http.MethodDelete:
		if id == "" {
			jsonError(w, http.StatusMethodNotAllowed, "DELETE method requires a booking ID")
			return
		}
		app.deleteBooking(w, r, id)
	default:
		jsonError(w, http.StatusMethodNotAllowed, fmt.Sprintf("Method %s not allowed", r.Method))
	}
}

func (app *application) listBookings(w http.ResponseWriter, r *http.Request) {
	userId := r.URL.Query().Get("userId")

	var rows pgx.Rows
	var err error

	baseSql := `SELECT id, flight_id, user_id, seat_number, status, created_at FROM bookings`

	if userId != "" {
		rows, err = app.db.Query(context.Background(), baseSql+" WHERE user_id = $1 ORDER BY created_at DESC", userId)
	} else {
		rows, err = app.db.Query(context.Background(), baseSql+" ORDER BY created_at DESC")
	}

	if err != nil {
		jsonError(w, http.StatusInternalServerError, "Database query failed")
		log.Printf("Error listing bookings: %s", err)
		return
	}
	defer rows.Close()

	bookings, err := pgx.CollectRows(rows, pgx.RowToStructByName[models.Booking])
	if err != nil {
		jsonError(w, http.StatusInternalServerError, "Failed to process booking data")
		log.Printf("Error collecting rows: %s", err)
		return
	}

	// Return empty list instead of null if no bookings found
	if bookings == nil {
		bookings = []models.Booking{}
	}

	writeJSON(w, http.StatusOK, bookings)
}

func (app *application) getBookingByID(w http.ResponseWriter, r *http.Request, id string) {
	var booking models.Booking
	sql := `SELECT id, flight_id, user_id, seat_number, status, created_at FROM bookings WHERE id = $1`

	err := app.db.QueryRow(context.Background(), sql, id).Scan(&booking.ID, &booking.FlightID, &booking.UserID, &booking.SeatNumber, &booking.Status, &booking.CreatedAt)

	if err != nil {
		if err == pgx.ErrNoRows {
			jsonError(w, http.StatusNotFound, "Booking not found")
		} else {
			jsonError(w, http.StatusInternalServerError, "Database error")
			log.Printf("Error querying for booking: %s", err)
		}
		return
	}

	writeJSON(w, http.StatusOK, booking)
}

func (app *application) createBookingHandler(w http.ResponseWriter, r *http.Request) {
	var input struct {
		FlightID   string `json:"flightId"`
		UserID     string `json:"userId"`
		SeatNumber string `json:"seatNumber"`
	}

	if err := json.NewDecoder(r.Body).Decode(&input); err != nil {
		jsonError(w, http.StatusBadRequest, "Invalid request body")
		return
	}

	newBooking := models.Booking{
		ID:         uuid.NewString(),
		FlightID:   input.FlightID,
		UserID:     input.UserID,
		SeatNumber: input.SeatNumber,
		Status:     "CONFIRMED",
		CreatedAt:  time.Now(),
	}

	sql := `INSERT INTO bookings (id, flight_id, user_id, seat_number, status, created_at) VALUES ($1, $2, $3, $4, $5, $6)`
	_, err := app.db.Exec(context.Background(), sql, newBooking.ID, newBooking.FlightID, newBooking.UserID, newBooking.SeatNumber, newBooking.Status, newBooking.CreatedAt)

	if err != nil {
		jsonError(w, http.StatusInternalServerError, "Failed to create booking")
		log.Printf("Error inserting booking: %s", err)
		return
	}

	writeJSON(w, http.StatusCreated, newBooking)
}

func (app *application) updateBookingStatus(w http.ResponseWriter, r *http.Request, id string) {
	var input struct {
		Status string `json:"status"`
	}

	if err := json.NewDecoder(r.Body).Decode(&input); err != nil {
		jsonError(w, http.StatusBadRequest, "Invalid request body")
		return
	}

	sql := `UPDATE bookings SET status = $1 WHERE id = $2`
	res, err := app.db.Exec(context.Background(), sql, input.Status, id)
	if err != nil {
		jsonError(w, http.StatusInternalServerError, "Failed to update booking")
		log.Printf("Error updating booking: %s", err)
		return
	}

	if res.RowsAffected() == 0 {
		jsonError(w, http.StatusNotFound, "Booking not found or no update was needed")
		return
	}

	w.WriteHeader(http.StatusNoContent) // Success, no content to return
}

func (app *application) deleteBooking(w http.ResponseWriter, r *http.Request, id string) {
	sql := `DELETE FROM bookings WHERE id = $1`
	res, err := app.db.Exec(context.Background(), sql, id)
	if err != nil {
		jsonError(w, http.StatusInternalServerError, "Failed to delete booking")
		log.Printf("Error deleting booking: %s", err)
		return
	}

	if res.RowsAffected() == 0 {
		jsonError(w, http.StatusNotFound, "Booking not found")
		return
	}

	w.WriteHeader(http.StatusNoContent) // Success, no content to return
}

// --- HELPER FUNCTIONS ---
func writeJSON(w http.ResponseWriter, status int, data interface{}) {
	w.Header().Set("Content-Type", "application/json")
	w.WriteHeader(status)
	if err := json.NewEncoder(w).Encode(data); err != nil {
		log.Printf("Error encoding JSON response: %s", err)
	}
}

func jsonError(w http.ResponseWriter, status int, message string) {
	writeJSON(w, status, map[string]string{"error": message})
}
