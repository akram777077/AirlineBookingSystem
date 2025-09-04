# Stage 1: Build the Go binary
FROM golang:1.21-alpine AS builder

WORKDIR /app

# Copy module files and download dependencies
COPY go.mod go.sum ./
RUN go mod download

# Copy the rest of the source code
COPY . .

# Build the application
WORKDIR /app/cmd/bookingservice
RUN CGO_ENABLED=0 GOOS=linux go build -o /main .

# Stage 2: Create the final, minimal image
FROM alpine:latest

WORKDIR /app

# Copy the binary from the builder stage
COPY --from=builder /main .

# Expose the port the service runs on
EXPOSE 8080

# Run the binary
ENTRYPOINT ["/main"]
