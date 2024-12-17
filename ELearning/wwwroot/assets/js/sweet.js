//function AlertError(text, title = "Error") {
//    Swal.fire({
//        title: title,
//        text: text,
//        confirmButtonColor: "#Ff0000",
//        confirmButtonWidth: 20,
//        confirmButtonText: "Close",
//    })
//}
window.onload = function () {
    Swal.fire({
        title: "Welcome!",
        text: "To Mega Courses Around World.",
        imageUrl: "./assets/images/welcome.svg",
        imageWidth: 300,
        imageHeight: 200,
        imageAlt: "Custom image",
        confirmButtonColor: "#000",
        confirmButtonWidth: 20,
        confirmButtonText: "Close",
    });
};
