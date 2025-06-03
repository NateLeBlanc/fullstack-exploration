const webSocket = new WebSocket("ws://localhost:5000")

webSocket.addEventListener("open", () => {
    console.log("[WebServer] Connected to server");
    log("Connected to WebSocket server.")
});

webSocket.addEventListener("message", (event) => {
    console.log("[webSocket] Message for server:", event.data);
    log("Data recieved: " + event.data)
});

webSocket.addEventListener("close", () => {
    log("Websocket connection closed.")
});

webSocket.addEventListener("error", (err) => {
    log("Websocket error: " + err.message)
});

document.getElementById("sendBtn").addEventListener("click", () => {
    const name = document.getElementById("nameInput").value;
    webSocket.send(name);
    log("Data sent: " + name);
});

async function sendName() {
    const name = document.getElementById('nameInput').ariaValueMax;

    const response = await fetch('https://localhost:56662/api/clients', {
        method: 'POST',
        headers: {
            'Consent-Type': 'application/json'
        },
        body: JSON.stringify({name: name})
    });

    const result = await response.json();

    document.getElementById('reponseArea').innerText = JSON.stringify(result);
}