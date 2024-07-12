(function () {
    addBiodataDialogListener();
    addSummaryDialogListener();
    addLoanDetailDialogListener();
    addCloseButtonListener();
    addInsertButtonListener();
    addUpdateButtonListener();
    addSubmitFormListener();
    addDeleteButtonListener();
    addSubmitDeleteButtonListener();
    addReturnDateListener();
}())

function addCloseButtonListener() {
    $('.close-button').click(function (event) {
        $('.modal-layer').removeClass('modal-layer--opened');
        $('.popup-dialog').removeClass('popup-dialog--opened');
        $('.popup-dialog input').val("");
        $('.popup-dialog textarea').val("");
        $('.popup-dialog .validation-message').text("");
    });
}

function addInsertButtonListener() {
    $('#create-button').click(function (event) {
        event.preventDefault();
        $('.modal-layer').addClass('modal-layer--opened');
        $('.form-dialog').addClass('popup-dialog--opened');
    });
}

function addSubmitFormListener() {
    $('.form-dialog button').click(function (event) {
        event.preventDefault();
        let formType = $(this).attr('data-form');
        let vm = (formType == "Loan") ? collectInputForm("Loan") : collectInputForm("Category");
        let requestMethod;
        if (formType == "Loan") {
            requestMethod = (vm.id === 0) ? 'POST' : 'PUT';
        } else {
            let hiddenValue = $('.form-dialog .filled').val();
            requestMethod = (hiddenValue == "filled") ? 'PUT' : 'POST';
        }
        let url;
        if (requestMethod == 'POST') {
            url = (formType == "Loan") ? 'http://localhost:5165/Loan/api' : 'http://localhost:5165/Category';
        } else {
            url = (formType == "Loan") ? `http://localhost:5165/Loan/api/${vm.id}` : `http://localhost:5165/Category/${vm.name}`;
        }
        $.ajax({
            method: requestMethod,
            url: url,
            data: JSON.stringify(vm),
            contentType: 'application/json',
            success: function (response) {
                location.reload();
            },
            error: function ({ status, responseJSON }) {
                if (status === 400) {
                    writeValidationMessage(responseJSON.errors);
                }
                else {
                    alert(`Ada error dengan status code: ${status}`);
                }
            }
        });
    })
}

function collectInputForm(type) { 
    let vm;
    if (type == "Loan") {
        let loanId = $('.form-dialog .id').val()
        vm = {
            id: (loanId === "") ? 0 : loanId,
            customerNumber: $('.form-dialog .customer').val(),
            bookCode: $('.form-dialog .book').val(),
            loanDate: $('.form-dialog .loanDate').val(),
            note: $('.form-dialog .note').val(),
        }
    } else {
        vm = {
            name: $('.form-dialog .categoryName').val(),
            floor: $('.form-dialog .floor').val(),
            isle: $('.form-dialog .isle').val(),
            bay: $('.form-dialog .bay').val(),
        };
    }
    return vm;
}

function writeValidationMessage(errorMessages) {
    let validationMessage = document.querySelectorAll('.validation-message');
    for (let validation of validationMessage) {
        validation.innerHTML = '';
    }
    Object.keys(errorMessages).forEach(field => {
        let messages = errorMessages[field];

        console.log(`Property: ${field}`);
        field = field.charAt(0).toLowerCase() + field.slice(1);
        messages.forEach(message => {
            console.log(message);
            $(`.form-dialog [data-for=${field}]`).text(message);
        });


    });
    //for(let error of errorMessages){
    //    let {field, message} = error;
    //    $(`.form-dialog [data-for=${field}]`).text(message);
    //}

}

function addUpdateButtonListener() {
    $('.update-button').click(function (event) {
        event.preventDefault();
        let formType = $(this).attr('data-form');
        let categoryName;
        let loanId;
        if (formType == "Loan") {
            loanId = $(this).attr('data-id');
        } else {
            categoryName = $(this).attr('data-name');
        }
        
        $.ajax({
            url: (formType == "Loan") ? `http://localhost:5165/Loan/api/${loanId}` : `http://localhost:5165/Category/${categoryName}`,
            method: 'GET',
            success: function (response) {
                populateInputForm(response);
                $('.modal-layer').addClass('modal-layer--opened');
                $('.form-dialog').addClass('popup-dialog--opened');
            }
        })
    });
    function populateInputForm(response) {
        let loanDate = new Date(response.loanDate);
        console.log(typeof (loanDate));
        loanDate = loanDate.toISOString().split('T')[0];
        console.log(typeof (loanDate));
        $('.form-dialog .filled').val("filled");
        $('.form-dialog .categoryName').val(response.name);
        $('.form-dialog .floor').val(response.floor);
        $('.form-dialog .isle').val(response.isle);
        $('.form-dialog .bay').val(response.bay);
        $('.form-dialog .customer').val(response.customerNumber);
        $('.form-dialog .book').val(response.bookCode);
        $('.form-dialog .loanDate').val(loanDate);
        $('.form-dialog .note').val(response.note);
    }

}

function addDeleteButtonListener() {
    $('#tableBody').on('click', '.delete-button', function (event) {
        event.preventDefault();
        let categoryName= $(this).attr('data-name');
        $('.delete-dialog .name').val(categoryName);
        $('.modal-layer').addClass('modal-layer--opened');
        $('.delete-dialog').addClass('popup-dialog--opened');
    });
}

function addSubmitDeleteButtonListener() {
    $('.delete-dialog button').click(function (event) {
        let categoryName = $('.delete-dialog .name').val();
        $.ajax({
            method: "DELETE",
            url: `http://localhost:5165/Category/${categoryName}`,
            success: function (response) {
                location.reload();
            },
            error: function (response) {
                alert(`Ada error dengan status code: ${response.status}`);
            }
        });
    });
}

function addBiodataDialogListener() {
    $('#tableBody').on('click', '.biodata-button', function (event) {
        let membershipNumber = $(this).attr('data-number');
        $.ajax({
            url: `http://localhost:5165/Customer/api/${membershipNumber}`,
            method: 'GET',
            success: function (customer) {
                let birthDate = new Date(customer.birthDate);
                birthDate = birthDate.toDateString();
                $('.biodata-dialog .membership-number').text(customer.membershipNumber);
                $('.biodata-dialog .full-name').text(customer.firstName + " " + customer.lastName);
                $('.biodata-dialog .birth-date').text(birthDate);
                $('.biodata-dialog .gender').text(customer.gender);
                $('.biodata-dialog .phone').text(customer.phone);
                $('.biodata-dialog .address').text(customer.address);
                $('.modal-layer').addClass('modal-layer--opened');
                $('.biodata-dialog').addClass('popup-dialog--opened');
            }
        });
    });
}

function addSummaryDialogListener() {
    $('#tableBody').on('click', '.summary-button', function (event) {
        let bookCode = $(this).attr('data-code');
        $.ajax({
            url: `http://localhost:5165/Book/api/${bookCode}`,
            method: 'GET',
            success: function (book) {
                $('.summary-dialog .summary').text(book.summary);
                $('.modal-layer').addClass('modal-layer--opened');
                $('.summary-dialog').addClass('popup-dialog--opened');
            }
        });
    });
}

function addLoanDetailDialogListener() {
    $('#tableBody').on('click', '.detail-button', function (event) {
        let loanId = $(this).attr('data-id');
        $.ajax({
            url: `http://localhost:5165/Loan/api/${loanId}`,
            method: 'GET',
            success: function (loan) {
                $('.detail-dialog .book-title').text(loan.book.title);
                $('.detail-dialog .category-name').text(loan.categoryName);
                $('.detail-dialog .author').text(loan.book.author.firstName);
                $('.detail-dialog .floor').text(loan.book.categoryNameNavigation.floor);
                $('.detail-dialog .isle').text(loan.book.categoryNameNavigation.isle);
                $('.detail-dialog .bay').text(loan.book.categoryNameNavigation.bay);
                $('.detail-dialog .member-number').text(loan.customer.membershipNumber);
                $('.detail-dialog .member-name').text(loan.customer.firstName);
                $('.detail-dialog .phone').text(loan.customer.phone);
                $('.detail-dialog .expire-date').text(loan.customer.membershipExpiredDate);
                $('.modal-layer').addClass('modal-layer--opened');
                $('.biodata-dialog').addClass('popup-dialog--opened');
            } 
        });

    });
}

function addReturnDateListener() {
    $('#tableBody').on('click', '.return-button', function (event) {
        let loanId = $(this).attr('data-id');
        $.ajax({
            url: `http://localhost:5165/Loan/api/${loanId}`,
            method: 'PATCH',
            success: function (response) {
                location.reload();
            }
        });
    });
}