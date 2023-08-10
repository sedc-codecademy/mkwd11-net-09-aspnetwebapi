let addNote = async () => {
    let inputElement = document.getElementById("addNoteInput");

    let url = "https://localhost:7015/" + "api/notes";

    let response = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'text/plain'
        },
        body: inputElement.value
    });

    let responseData = await response.text();
    alert(responseData);
}

let buttonElement = document.getElementById("addNoteBtn");
buttonElement.addEventListener("click", addNote);