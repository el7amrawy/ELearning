// Toggle user dropdown
const userMenuButton = document.getElementById('user-menu-button');
const userDropdown = document.getElementById('user-dropdown');

userMenuButton.addEventListener('click', () => {
    userDropdown.classList.toggle('hidden');
});

// Close dropdown when clicking outside
document.addEventListener('click', (event) => {
    if (!userMenuButton.contains(event.target) && !userDropdown.contains(event.target)) {
        userDropdown.classList.add('hidden');
    }
});

// Toggle mobile menu
const mobileMenuButton = document.getElementById('mobile-menu-button');
const mobileMenu = document.getElementById('mobile-menu');

//console.log(mobileMenuButton.children)
mobileMenuButton.addEventListener('click', () => {
    mobileMenu.classList.toggle('hidden');
    Array.from(mobileMenuButton.children).forEach(ele => ele.classList.toggle("hidden"))
});

document.addEventListener('click', (event) => {
    if (!mobileMenu.contains(event.target) && !mobileMenuButton.contains(event.target)) {
        if (!mobileMenu.classList.contains("hidden")) Array.from(mobileMenuButton.children).forEach(ele => ele.classList.toggle("hidden"))
        mobileMenu.classList.add('hidden');
    }
});