let payment = document.querySelectorAll(".payment");
let price_value = 0;
let total_value = 0;

payment.forEach(function(ele){
    ele.addEventListener('click' , function(){
        payment.forEach((ele) => ele.children[1].classList.add("hidden"));
        this.children[1].classList.toggle("hidden");
    })
})

let courses_bought = document.querySelector(".courses_bought");

if(window.localStorage.getItem("cartItems")){
    let course = window.localStorage.getItem("cartItems");
    console.log(JSON.parse(course))
    JSON.parse(course).forEach(function(ele) {
        courses_bought.appendChild(display_courses_bought(ele));
        price_value += ele.price;
    });

    let price = document.querySelector(".price");
    let discount = document.querySelector(".discount");
    let total = document.querySelector(".total");
    if(discount.children[1].childNodes[1].innerHTML !== "00"){
        total_value = price_value - price_value * Number(discount.children[1].childNodes[1].innerHTML)/100;
    }else{
        total_value = price_value;
    }
    
    price.children[1].childNodes[1].innerHTML = price_value;
    total.children[1].childNodes[1].innerHTML = total_value;
}

function display_courses_bought(courses){
    let father = document.createElement("div");
    father.className = "lg:flex border min-h-24 justify-between mb-3 p-2 rounded items-start";

    /*  collect the image and title */
    let div = document.createElement("div");
    div.className = "flex";

    let child_image = document.createElement("div");
    child_image.className = "col-span-1"
    let image = document.createElement("img");
    image.style.cssText = "width:150px"
    image.src = courses.image;
    child_image.appendChild(image)
    div.appendChild(child_image);

    let child_title = document.createElement("div");
    child_title.className = "w-full"
    let title = document.createElement("p");
    title.className = "text-sm p-2"
    title.innerHTML = courses.title;
    child_title.appendChild(title);
    div.appendChild(title);

    father.appendChild(div)

    let child_price = document.createElement("div");
    child_price.className="p-2 min-w-28 mt-2"
    let price = document.createElement("p");
    price.innerHTML = `$<span>${courses.price}</span>`;
    price.className = "text-center";

    child_price.appendChild(price);
    father.appendChild(child_price);

    return father;
}



