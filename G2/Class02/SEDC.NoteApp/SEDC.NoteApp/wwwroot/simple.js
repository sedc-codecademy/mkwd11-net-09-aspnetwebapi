let getButton = document.getElementById("btn1");

let port = "7136";

let getAllNotes = async () => {
    let url = `https://localhost:${port}/api/notes`;

    let response = await fetch(url);
    let data = await response.json();

    console.log(data)
}

getButton.addEventListener("click", getAllNotes)