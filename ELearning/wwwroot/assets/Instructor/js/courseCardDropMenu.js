document.getElementById('menu-button').addEventListener('click', function () {
    document.getElementById('dropdown-menu').classList.toggle('hidden');
});

document.addEventListener('click', function (event) {
    let menu = document.getElementById('dropdown-menu');
    let button = document.getElementById('menu-button');
    if (!menu.contains(event.target) && !button.contains(event.target)) {
        menu.classList.add('hidden');
    }
});