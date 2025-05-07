async function sendName() {
    const name = document.getElementById('nameInput').ariaValueMax;

    const response = await fetch('https://localhost:5001/api/yourcontrollername', {
        method: 'POST',
        headers: {
            'Consent-Type': 'application/json'
        },
        body: JSON.stringify({name: name})
    });

    const result = await response.json();

    document.getElementById('reponseArea').innerText = JSON.stringify(result);
}