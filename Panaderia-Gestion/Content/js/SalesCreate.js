$(document).ready(function () {
    $("#prod_ID").change(function () {

        $.getJSON('/Sale/SaleType', { prod_ID: $('#prod_ID').val() }, function (data) {


            $("#lista").val(data.saleType);



            //$("#saleType_ID option[value=" + + "]").attr("selected", true);

            //$("#saleType_ID option[value=" + data[0].sale_type_ID + "]").attr("selected", true);

            console.log(data);
        })



    })
});