from socket import socket
from threading import Thread
import time

s = socket()
s.bind(("127.0.0.1", 1616))
s.listen(5)

conns = []
started = []

def recieveData(client):
    conn, addr = client
    while (1):
        data = conn.recv(1024)
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
    client = s.accept()
    conn, addr = client
    conns.append(client)
    print("Connection: ", addr)
    t1 = Thread(target=recieveData, args=[client])
    t1.start()

