// Get the modal
var modal = document.getElementById("NoteModal");

// Get the button that opens the modal
var btn = document.getElementById("addNoteButton");

// Get the <span> element that closes the modal
var span = document.getElementsByClassName("close")[0];

// When the user clicks on the button, open the modal
btn.onclick = function () {
    modal.style.display = "block";
}

// When the user clicks on <span> (x), close the modal
span.onclick = function () {
    modal.style.display = "none";
}

// When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}

const form = document.getElementById("addNoteForm");

form.addEventListener("submit", function (event) {
    event.preventDefault();

    const title = document.getElementById("newNoteTitle").value;
    const date = document.getElementById("newNoteDate").value;
    const image = document.getElementById("newNoteImage").files[0];
    const detail = document.getElementById("newNoteDetail").value;

    const formData = new FormData();
    formData.append("Title", title);
    formData.append("Date", date);
    formData.append("Image", image);
    formData.append("Detail", detail);

    const xhr = new XMLHttpRequest();
    xhr.open("POST", "/NoteBook/Add_Note", true);
    xhr.onreadystatechange = function () {
        if (xhr.readyState === XMLHttpRequest.DONE) {
            if (xhr.status === 200) {
                const response = xhr.responseText;
                console.log(response);
                var taskTable = document.querySelector('#NoteTable');

                // Get the values of the input fields
                var title = document.querySelector('#newNoteTitle').value;
                var date = document.querySelector('#newNoteDate').value;
                var imageInput = document.querySelector('#newNoteImage');
                var detail = document.querySelector('#newNoteDetail').value;

                // Create a new row for the table with the values obtained
                var newRow = taskTable.insertRow();
                var imageCell = newRow.insertCell();
                var dateCell = newRow.insertCell();
                var detailCell = newRow.insertCell();
                var actionsCell = newRow.insertCell();
                // Read the selected file and convert it to a data URL
                var reader = new FileReader();
                reader.readAsDataURL(imageInput.files[0]);
                reader.onload = function () {
                    // Create an image element with the data URL and append it to the image cell
                    var image = document.createElement('img');
                    image.src = reader.result;
                    image.classList.add('shadow-1-strong', 'rounded-circle');
                    image.alt = 'avatar';
                    image.style.width = '55px';
                    image.style.height = 'auto';

                    var span = document.createElement('span');
                    span.classList.add('ms-2');
                    span.textContent = title;

                    var th = document.createElement('th');
                    th.appendChild(image);
                    th.appendChild(span);
                    imageCell.appendChild(th);
                    dateCell.innerHTML = date;
                    actionsCell.innerHTML = `<a href="/Notebook/Edit_Note/${response}" data-mdb-toggle='tooltip' title='Edit'><i class='fas fa-pencil-square-o text-success me-3'></i></a><a href="#" onclick="delet(this,${response})" data-mdb-toggle='tooltip' title='Remove'><i class='fas fa-trash-alt text-danger'></i></a>`;
                    detailCell.innerHTML = `<h6 class="mb-0"><span class="badge bg-danger">${detail}</span></h6>`;
                };



                // Clear the input fields
                document.querySelector('#newNoteTitle').value = '';
                document.querySelector('#newNoteDate').value = '';
                document.querySelector('#newNoteImage').value = '';
                document.querySelector('#newNoteDetail').value = '';

                // Hide the modal
                var modal = document.getElementById('NoteModal');
                modal.style.display = 'none';
                // Handle the success response
            } else {
                console.log("Error: " + xhr.status);
                // Handle the error response
            }
        }
    };
    xhr.send(formData);
});
//form.addEventListener('submit', (event) => {
//    // Prevent the default form submission behavior
//    event.preventDefault();
//    // Get the table element
//    var taskTable = document.querySelector('#NoteTable');

//    // Get the values of the input fields
//    var title = document.querySelector('#newNoteTitle').value;
//    var date = document.querySelector('#newNoteDate').value;
//    var imageInput = document.querySelector('#newNoteImage');
//    var detail = document.querySelector('#newNoteDetail').value;

//    // Create a new row for the table with the values obtained
//    var newRow = taskTable.insertRow();
//    var imageCell = newRow.insertCell();
//    var dateCell = newRow.insertCell();
//    var detailCell = newRow.insertCell();
//    var actionsCell = newRow.insertCell();
//    // Read the selected file and convert it to a data URL
//    var reader = new FileReader();
//    reader.readAsDataURL(imageInput.files[0]);
//    reader.onload = function () {
//        // Create an image element with the data URL and append it to the image cell
//        var image = document.createElement('img');
//        image.src = reader.result;
//        image.classList.add('shadow-1-strong', 'rounded-circle');
//        image.alt = 'avatar';
//        image.style.width = '55px';
//        image.style.height = 'auto';

//        var span = document.createElement('span');
//        span.classList.add('ms-2');
//        span.textContent = title;

//        var th = document.createElement('th');
//        th.appendChild(image);
//        th.appendChild(span);
//        imageCell.appendChild(th);
//        dateCell.innerHTML = date;
//        actionsCell.innerHTML = "<a href='#!' data-mdb-toggle='tooltip' title='Done'><i class='fas fa-pencil-square-o text-success me-3'></i></a><a href='#!' data-mdb-toggle='tooltip' title='Remove'><i class='fas fa-trash-alt text-danger'></i></a>";
//        detailCell.innerHTML = `<h6 class="mb-0"><span class="badge bg-danger">${detail}</span></h6>`;
//    };



//    // Clear the input fields
//    document.querySelector('#newNoteTitle').value = '';
//    document.querySelector('#newNoteDate').value = '';
//    document.querySelector('#newNoteImage').value = '';
//    document.querySelector('#newNoteDetail').value = '';

//    // Hide the modal
//    var modal = document.getElementById('NoteModal');
//    modal.style.display = 'none';
//});


// Get the button that saves the new task

function delet(row, Id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/Notebook/Delete_Note",
                type: "Get",
                data: {
                    id: Id
                }
            })
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Note has been deleted !',
                showConfirmButton: false,
                timer: 1500
            })
            $(row).closest('tr').empty();
        }
    })
}