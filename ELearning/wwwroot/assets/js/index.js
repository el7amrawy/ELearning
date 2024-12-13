let menu = document.querySelector("#mobile-menu");
let icon_menu = document.querySelector(".menu-icon")
let user_menu = document.querySelector("[role=menu]")
let user_icon = document.querySelector("#user-menu-button");
let search_icon = document.querySelector(".search_icon");
let search_input = document.querySelector(".search_input");
let other_search = document.querySelector(".other-search");


/* ===============>>> btn Open Menu <<<=============== */
if(icon_menu){
    icon_menu.addEventListener('click',function(){
        if(search_input.children[0].className === "active"){
            search_input.classList.toggle("hidden");
            search_input.children[0].classList.toggle("active");
        }
        if(user_icon.parentElement.parentElement.children[1].children[0].className === "active"){
            user_icon.parentElement.parentElement.children[1].children[0].classList.toggle("active");
            user_menu.classList.toggle("hidden");
        }
        menu.classList.toggle("hidden");
        menu.children[0].classList.toggle("active");
        icon_menu.childNodes[5].classList.toggle("hidden");
        icon_menu.childNodes[7].classList.toggle("hidden");
    })
}
/* ===============>>> btn Open User <<<=============== */
user_icon.addEventListener('click' ,function(){
    if(search_input.children[0].className === "active"){
        search_input.classList.toggle("hidden");
        search_input.children[0].classList.toggle("active");
    }
    if(menu.children[0].className === "active"){
        menu.classList.toggle("hidden");
        icon_menu.childNodes[5].classList.toggle("hidden");
        icon_menu.childNodes[7].classList.toggle("hidden");
        menu.children[0].classList.toggle("active");
    }
    user_icon.parentElement.parentElement.children[1].children[0].classList.toggle("active");
    user_menu.classList.toggle("hidden");
})
/* ==============>>> btn open search <<<============== */
search_icon.addEventListener('click' , function(){
    if(menu.children[0].className === "active"){
        menu.classList.toggle("hidden");
        icon_menu.childNodes[5].classList.toggle("hidden");
        icon_menu.childNodes[7].classList.toggle("hidden");
        menu.children[0].classList.toggle("active");
    }
    if(user_icon.parentElement.parentElement.children[1].children[0].className === "active"){
        user_icon.parentElement.parentElement.children[1].children[0].classList.toggle("active");
        user_menu.classList.toggle("hidden");
    }
    search_input.classList.toggle("hidden");
    search_input.children[0].classList.toggle("active");
})
/* ================>>> Other search <<<=============== */
if(other_search)other_search.addEventListener('click' , function(){
    if(menu.children[0].className === "active"){
        menu.classList.toggle("hidden");
        icon_menu.childNodes[5].classList.toggle("hidden");
        icon_menu.childNodes[7].classList.toggle("hidden");
        menu.children[0].classList.toggle("active");
    }
    if(user_icon.parentElement.parentElement.children[1].children[0].className === "active"){
        user_icon.parentElement.parentElement.children[1].children[0].classList.toggle("active");
        user_menu.classList.toggle("hidden");
    }
    search_input.classList.toggle("hidden");
    search_input.children[0].classList.toggle("active");
})

/* ================>>> Focus Data <<<================= */
let focus_data = document.querySelectorAll(".focus_data .data");
let details = document.querySelectorAll(".view_Details img");

if(focus_data)focus_data.forEach(function(ele){
    ele.addEventListener('click' ,function(){
        focus_data.forEach(function(focus){
            focus.classList.remove("border-sky-500");
        })
        details.forEach(function(img){
            if(img.getAttribute("data") !== ele.getAttribute("focus")){
                img.classList.add("hidden");
            }else{
                img.classList.remove("hidden");
            }
        })
        this.classList.add("border-sky-500");
    })
})

/* ============>>> options <<<============== */
let options = document.querySelectorAll(".options .option");
options.forEach(function(option){
    option.addEventListener('click' , function(){
        this.children[1].classList.toggle("h-0");
    })
})
