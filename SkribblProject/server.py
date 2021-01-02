import socket
from threading import Thread
import time

s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
s.bind(("0.0.0.0", 60000))

conns = []
started = []

def recieveData(client):
    conn, addr = client
    while (1):
        data = conn.recvfrom(1024)
        print (data.decode("UTF-8"))
        if data == "":
            print("Client disconnected :(")
            conns.remove(client)
            break

        for c in conns:
            if c != client:
                conn2, addr2 = c
                conn2.send(data)

print("Listening...")

while (1):
    data, addr = s.recvfrom(1024)
    print("Connection: ", addr)
    if (data == "viewer"):
        conns.append(addr)
        continue
    print("Data: ", data)
    for a in conns:
        print(a)
        s.sendto(data, a)

