
function deleteProduct() {
    var productEntry = $(this).closest(".product-entry");
    productEntry.remove();
}
function goBack() {
    history.back();
}