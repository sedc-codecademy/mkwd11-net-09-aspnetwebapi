const BASE_URL = 'https://localhost:7208/api/v1/note'
const headers = {
    'Accept': 'application/json, text/plain',
    'Content-Type': 'application/json;charset=UTF-8'
}
const btn = document.querySelector("#create-note-btn");
btn.addEventListener("click", function () {
    const input = document.querySelector("#create-note")
    const value = input.value;
    fetch(BASE_URL, {
        method: 'post',
        body: '"' + value + '"',
        headers,
    }).then(x => {
        refreshList();
    })
})

function refreshList() {
    fetch(BASE_URL, {
        method: 'get',
    }).then(x => x.json())
        .then(res => {
            const list = document.querySelector("#list-note");
            let html = '';
            res.forEach(x => {
                html += `<div> ${x.title} - ${x.tags.map(x => x.name).join('-')} <button class="add-tag" data-note-id="${x.id}">Add tag </button></div>`
            })
            list.innerHTML = html;
            document.querySelectorAll('.add-tag').forEach(btn => {
                btn.addEventListener("click", function () {
                    const tagName = prompt("Provide me with the tag name");
                    const id = btn.dataset.noteId;
                    fetch(`${BASE_URL}/${id}/tags`, {
                        headers,
                        method: 'post',
                        body: JSON.stringify({
                            id: 0,
                            Name: tagName
                        })
                    })
                        .then(x => {
                            refreshList();
                        })
                })
            })
        })
}

refreshList();