var total = 0;

function ImageClick(imageId) {
    var id = GetNumericID(imageId);
    $.ajax({
        type: "POST",
        url: '/Home/AddProductToCart',
        data: { product: id},
        success: function (result) {
            if (result == -1) {
                alert('Not in stock');
            }
            else {
                GetStocks()
            }
            GetTotalPrice();
        }
    });
}

function Reset() {   
    $.ajax({
        type: "POST",
        url: '/Home/Reset',
        success: function (result) {
            GetStocks();
            GetTotalPrice();
        }
    });    

}

function Checkout() {
    if (total == 0) {
        alert("You didn't select any product");
    }
    var amount = prompt('Total price is ' + (total/100).toFixed(2) + '€. Please insert money', '0.00')*100;
    if (!isNumeric(amount)) {
        alert('Enter a numeric value');
    }
    else {
        if (amount < total) {
            alert('Not enough money inserted. Please retry.');
        }
        else {
            $.ajax({
                type: "GET",
                url: '/Home/Checkout',
                data: {
                    Amount: amount,
                    Cost: total
                },
                success: function (result) {
                    alert('Money returned : ' + result);
                    GetTotalPrice();
                }

            });
        }
    }
}

function GetNumericID(id) {
    switch (id) {
        case 'img_brownie':
            return 1;
        case 'img_muffin':
            return 2;
        case 'img_cakePop':
            return 3;
        case 'img_appleTart':
            return 4;
        case 'img_water':
            return 5;
        case 'img_shirt':
            return 6;
        case 'img_pants':
            return 7;
        case 'img_jacket':
            return 8;
        case 'img_toy':
            return 9;
    }
}

function GetIDFromNumeric(id) {
    switch (id) {
        case 1:
            return 'img_brownie';
        case 2:
            return 'img_muffin';
        case 3:
            return 'img_cakePop';
        case 4:
            return 'img_appleTart';
        case 5:
            return 'img_water';
        case 6:
            return 'img_shirt';
        case 7:
            return 'img_pants';
        case 8:
            return 'img_jacket';
        case 9:
            return 'img_toy';
    }
}


function OutOfStock(img) {
    document.getElementById(img).style.webkitFilter = "grayscale(100 %)";
    document.getElementById(img).style.filter = "grayscale(100%)";

}

function InStock(img) {
    document.getElementById(img).style.webkitFilter = "grayscale(0%)";
    document.getElementById(img).style.filter = "grayscale(0%)";
}

$(document).ready(function () {
    GetStocks();
});

function GetStocks() {
    $.ajax({
        type: "GET",
        url: '/Home/GetStocks',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            var datas = result;
            for (var i = 0; i < datas.length; i++) {
                if (datas[i].stock == 0) {
                    OutOfStock(GetIDFromNumeric(datas[i].id));
                }
                else {
                    InStock(GetIDFromNumeric(datas[i].id));
                }
            }
        }
    });
}

function GetTotalPrice() {
    $.ajax({
        type: "GET",
        url: '/Home/GetTotalPrice',
        success: function (result) {
            total = result;
            document.getElementById('total').innerHTML = 'Total : ' + (total/100).toFixed(2) + '€';
        }
    });
}

function isNumeric(num) {
    return !isNaN(num)
}