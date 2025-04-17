let search_icon = document.querySelector(".search_icon");
let search_input = document.querySelector(".search_input");
let other_search = document.querySelector(".other-search");

 //===============>>> btn Open Menu <<<=============== 
const mobileMenuButton = document.getElementById('mobile-menu-button');
const mobileMenu = document.getElementById('mobile-menu');

//console.log(mobileMenuButton.children)
if (mobileMenuButton) mobileMenuButton.addEventListener('click', () => {
    mobileMenu.classList.toggle('hidden');
    Array.from(mobileMenuButton.children).forEach(ele => ele.classList.toggle("hidden"))
});

document.addEventListener('click', (event) => {
    if (mobileMenu && mobileMenuButton) {
        if (!mobileMenu.contains(event.target) && !mobileMenuButton.contains(event.target)) {
            if (!mobileMenu.classList.contains("hidden")) Array.from(mobileMenuButton.children).forEach(ele => ele.classList.toggle("hidden"))
            mobileMenu.classList.add('hidden');
        }
    }
});
 //Toggle user dropdown
const userMenuButton = document.getElementById('user-menu-button');
const userDropdown = document.getElementById('user-dropdown');

userMenuButton.addEventListener('click', () => {
    if (userDropdown) userDropdown.classList.toggle('hidden');
});

 //Close dropdown when clicking outside
document.addEventListener('click', (event) => {
    if (!userMenuButton.contains(event.target) && !userDropdown.contains(event.target)) {
        userDropdown.classList.add('hidden');
    }
});

 //==============>>> btn open search <<<============== 
if (search_icon) search_icon.addEventListener('click', function () {
    search_input.classList.toggle("hidden");
    search_input.children[0].classList.toggle("active");
})
 //================>>> Other search <<<=============== 
if (other_search) other_search.addEventListener('click', function () {
    search_input.classList.toggle("hidden");
    search_input.children[0].classList.toggle("active");
})

document.addEventListener('click', (event) => {
    if (search_icon && search_input) {
        if (!search_icon.contains(event.target) && !search_input.contains(event.target)) {
            search_input.classList.add('hidden');
        }
    }
});