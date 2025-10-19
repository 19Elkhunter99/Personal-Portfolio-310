import socket

HOST = '127.0.0.1'  # Server IP
PORT = 65432        # Server Port

def report_sighting():
    print("Welcome to the Idaho Wildlife Sighting Reporter!")
    species = input("Enter species (e.g., Moose, Elk): ")
    location = input("Enter location (e.g., Island Park): ")
    time = input("Enter time of sighting (e.g., 3:45 PM): ")

    message = f"{species} sighted at {location} around {time}"

    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
        s.connect((HOST, PORT))
        s.sendall(message.encode())
        response = s.recv(1024)
        print("Server response:", response.decode())

if __name__ == "__main__":
    report_sighting()
