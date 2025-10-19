import socket
import threading

HOST = '127.0.0.1'  # Localhost
PORT = 65432        # Port to listen on

sightings = []  # Store all wildlife reports

def handle_client(conn, addr):
    print(f"[NEW CONNECTION] {addr} connected.")
    with conn:
        while True:
            data = conn.recv(1024)
            if not data:
                break
            message = data.decode()
            print(f"[RECEIVED] {addr}: {message}")
            sightings.append((addr, message))
            conn.sendall("Sighting received!".encode())

def start_server():
    print("[STARTING] Wildlife Sighting Server is starting...")
    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as server:
        server.bind((HOST, PORT))
        server.listen()
        print(f"[LISTENING] Server is listening on {HOST}:{PORT}")
        while True:
            conn, addr = server.accept()
            thread = threading.Thread(target=handle_client, args=(conn, addr))
            thread.start()
            print(f"[ACTIVE CONNECTIONS] {threading.active_count() - 1}")

if __name__ == "__main__":
    start_server()
