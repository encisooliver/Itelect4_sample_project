$('input.number').on("blur", function (e) {
    nStr = this.value;
    nStr += '';

    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';

    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    var $this = $(this);

    var realValue = (x1 + x2);
    var thisValue = (x1 + x2).replace(/\,/g, '');
    var returnValue = 0;

    if (thisValue % 1 != 0) {
        if ((realValue.split('.')[1] || []).length == 1) {
            returnValue = parseFloat(thisValue).toFixed(2).toLocaleString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        } else {
            returnValue = realValue.toLocaleString(undefined, { maximumFractionDigits: 5 });
        }
    } else {
        returnValue = parseFloat(thisValue).toFixed(2).toLocaleString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    }

    $this.val(returnValue);
});

$("input.number").keydown(function (e) {
    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190, 189]) !== -1 ||
        (e.keyCode == 65 && e.ctrlKey === true) ||
        (e.keyCode == 67 && e.ctrlKey === true) ||
        (e.keyCode == 88 && e.ctrlKey === true) ||
        (e.keyCode >= 35 && e.keyCode <= 39)) {
        return;
    }

    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
        e.preventDefault();
    }
});

function formatDecimalValues(decimalValue) {
    var realValue = parseFloat(decimalValue).toString();
    var replaceValue = parseFloat(decimalValue).toString().replace(/\,/g, '');
    if (replaceValue % 1 != 0) {
        if ((realValue.split('.')[1] || []).length == 1) {
            return parseFloat(replaceValue).toFixed(2).toLocaleString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        } else {
            return parseFloat(realValue).toLocaleString(undefined, { maximumFractionDigits: 5 });
        }
    } else {
        return parseFloat(replaceValue).toFixed(2).toLocaleString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    }
}