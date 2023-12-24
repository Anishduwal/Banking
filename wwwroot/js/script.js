document.addEventListener("DOMContentLoaded", function () {
    const tableBody = document.getElementById("table-body");
    const addRowButton = document.getElementById("add-row");

    // Function to create a new row
    function createRow() {
        debugger;

        const row = document.createElement("tr");
        const textCell = document.createElement("td");
        const dropdownCell = document.createElement("td");

        const textField = document.createElement("input");
        textField.type = "text";
        textCell.appendChild(textField);

        const dropdown = document.createElement("select");
        // Populate dropdown options here
        const dropdownOptions = ["INT", "VARCHAR(50)", "VARCHAR(100)", "VARCHAR(255)", "VARCHAR(MAX)", "NVARCHAR(50)","NVARCHAR(100)","NVARCHAR(255)","NVARCHAR(MAX)","Decimal(18,2)","DateTime","BIT"];
        dropdownOptions.forEach(optionText => {
            const option = document.createElement("option");
            option.text = optionText;
            dropdown.appendChild(option);
        });
        dropdownCell.appendChild(dropdown);

        row.appendChild(textCell);
        row.appendChild(dropdownCell);
        tableBody.appendChild(row);
    }
    function collectData() {
        const rows = document.querySelectorAll("#table-body tr");
        const rowData = [];

        rows.forEach(row => {
            const textField = row.querySelector("input[type='text']");
            const dropdown = row.querySelector("select");

            rowData.push({
                FieldName : textField.value,
                DataType: dropdown.value
            });
        });

        return rowData;
    }
    // Attach event listener to the "Add Row" button
    addRowButton.addEventListener("click", createRow);


    $("#btnSubmit").click(function () {
        debugger;
        const data = collectData();
        var jsondata = JSON.stringify(data);
        $("#Jsondata").val(jsondata);

    })


});