from socket import socket
from threading import Thread

s = socket()
s.bind(("127.0.0.1", 1616))
s.listen(5)

conns = []
started = []

def getConnection():
    while (1):
        conn, addr = s.accept()
        conns.insert((conn,addr))
        print("Connection accepted")

def recieveData(conn, addr):
    while (1):
        data = conn.recv(1024).decode("UTF-8")
        if data == "":
            print("Client disconnected :(")
            conns.remove((conn, addr))
            break

        for conn2, addr2 in conns:
            if conns not in conns:
                s.sendto(data.encode("UTF-8"), addr2)

print("Listening...")

conn, addr = s.accept()

print("Connection: ", addr)

t1 = Thread(target=getConnection)
t1.start()
while (1):
    for conn, addr in conns:
        if (conn , addr) not in started:
            t2 = Thread(target=recieveData, args=(conn, addr))
            t2.start()
            started.append((conn,addr))

