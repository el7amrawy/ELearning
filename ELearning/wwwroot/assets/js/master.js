let menu = document.querySelector("#mobile-menu");
let icon_menu = document.querySelector(".menu-icon")

let user_menu = document.querySelector("[role=menu]")
let user_icon = document.querySelector("#user-menu-button");

if(icon_menu){
    icon_menu.addEventListener('click',function(){
        menu.classList.toggle("hidden");
        icon_menu.childNodes[5].classList.toggle("hidden");
        icon_menu.childNodes[7].classList.toggle("hidden")
    })
}
user_icon.addEventListener('click' ,function(){
    user_menu.classList.toggle("hidden")
})

/* ================>>> Register <<<================= */

const form = document.getElementById('form');
const username = document.getElementById('username');
const second = document.getElementById('second');
const email = document.getElementById('email');
const password = document.getElementById('password');

form.addEventListener('submit', e => {
    e.preventDefault();

    validateInputs();
});

const setError = (element, message) => {
    const inputControl = element.parentElement;
    const errorDisplay = inputControl.querySelector('.error');

    errorDisplay.innerText = message;
    inputControl.classList.add('error');
    inputControl.classList.remove('success')
}

const setSuccess = element => {
    const inputControl = element.parentElement;
    const errorDisplay = inputControl.querySelector('.error');

    errorDisplay.innerText = '';
    inputControl.classList.add('success');
    inputControl.classList.remove('error');
};

const isValidEmail = email => {
    const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}

const validateInputs = () => {
    const usernameValue = username.value.trim();
    const secondValue = second.value.trim();
    const emailValue = email.value.trim();
    const passwordValue = password.value.trim();

    if(usernameValue === '') {
        setError(username, 'Username is required');
    } else {
        setSuccess(username);
    }
    if(secondValue === '') {
        setError(second, 'second is required');
    } else {
        setSuccess(second);
    }

    if(emailValue === '') {
        setError(email, 'Email is required');
    } else if (!isValidEmail(emailValue)) {
        setError(email, 'Provide a valid email address');
    } else {
        setSuccess(email);
    }

    if(passwordValue === '') {
        setError(password, 'Password is required');
    } else if (passwordValue.length < 8 ) {
        setError(password, 'Password must be at least 8 character.')
    } else {
        setSuccess(password);
    }
};
