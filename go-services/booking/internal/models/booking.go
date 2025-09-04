package models

import "time"

// Booking represents a booking record in the database.
type Booking struct {
	ID         string    `json:"id"`
	FlightID   string    `json:"flightId"`
	UserID     string    `json:"userId"`
	SeatNumber string    `json:"seatNumber"`
	Status     string    `json:"status"`
	CreatedAt  time.Time `json:"createdAt"`
}
