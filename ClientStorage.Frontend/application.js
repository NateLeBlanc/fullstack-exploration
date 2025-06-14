const webSocket = new WebSocket("ws://localhost:5000/ws")

webSocket.addEventListener("open", () => {
    console.log("[WebServer] Connected to server");
    console.log("Connected to WebSocket server.")
});

webSocket.addEventListener("message", (event) => {
    console.log("[webSocket] Message for server:", event.data);
    console.log("Data received: " + event.data)
});

webSocket.addEventListener("close", () => {
    console.log("Websocket connection closed.")
});

webSocket.addEventListener("error", (err) => {
    console.log("Websocket error: " + err.message)
});

document.getElementById("sendBtn").addEventListener("click", async () => {
    const firstName = document.getElementById("firstNameInput").value;
    const lastName = document.getElementById("lastNameInput").value;
    const age = document.getElementById("ageInput").value;

    await fetch('http://localhost:5000/api/clients', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            firstName: firstName,
            lastName: lastName,
            age: age
        })
    });
});

document.getElementById("getBtn").addEventListener("click", async() => {
    const response = await fetch('http://localhost:5000/api/clients', {
        method: 'GET'
    });

    const result = await response.json();
    document.getElementById('responseArea').innerText = JSON.stringify(result, null, 2);
})

document.getElementById("deleteBtn").addEventListener("click", async() => {
    const clientId = document.getElementById("clientId").value;

    await fetch('http://localhost:5000/api/clients', {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json'
        },
        body: clientId
    });
})