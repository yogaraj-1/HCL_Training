
const studentForm = document.getElementById("studentForm");
const studentList = document.getElementById("studentList");
const total = document.getElementById("total");
const search = document.getElementById("search");
const emptyMessage = document.getElementById("emptyMessage");

// Load saved students from the browser
let students = JSON.parse(
    localStorage.getItem("students") || "[]"
);

function saveStudents() {
    localStorage.setItem("students", JSON.stringify(students));
}

function displayStudents(filter = "") {
    studentList.innerHTML = "";

    const filteredStudents = students.filter(student =>
        `${student.name} ${student.roll} ${student.email} ${student.department}`
            .toLowerCase()
            .includes(filter.toLowerCase())
    );

    filteredStudents.forEach(student => {
        const row = document.createElement("tr");

        [
            student.name,
            student.roll,
            student.email,
            student.department
        ].forEach(value => {
            const cell = document.createElement("td");
            cell.textContent = value;
            row.appendChild(cell);
        });

        const actionCell = document.createElement("td");
        const deleteButton = document.createElement("button");

        deleteButton.textContent = "Delete";
        deleteButton.className = "delete-btn";

        deleteButton.addEventListener("click", () => {
            students = students.filter(s => s.id !== student.id);
            saveStudents();
            displayStudents(search.value);
        });

        actionCell.appendChild(deleteButton);
        row.appendChild(actionCell);
        studentList.appendChild(row);
    });

    total.textContent = students.length;
    emptyMessage.style.display =
        filteredStudents.length === 0 ? "block" : "none";

    emptyMessage.textContent = students.length === 0
        ? "No students added yet."
        : "No matching students found.";
}

// Add a new student
studentForm.addEventListener("submit", function (event) {
    event.preventDefault();

    const name = document.getElementById("name").value.trim();
    const roll = document.getElementById("roll").value.trim();
    const email = document.getElementById("email").value.trim();
    const department = document.getElementById("department").value.trim();

    if (students.some(s => s.roll.toLowerCase() === roll.toLowerCase())) {
        alert("This roll number already exists!");
        return;
    }

    students.push({
        id: Date.now(),
        name,
        roll,
        email,
        department
    });

    saveStudents();
    displayStudents(search.value);
    studentForm.reset();
});

// Search students
search.addEventListener("input", function () {
    displayStudents(search.value);
});

// Show saved students when the page opens
displayStudents();
