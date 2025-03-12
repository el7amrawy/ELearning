// active classes of setting links
//edu profile

let settingLinks = document.querySelectorAll(".setting-links ul li a");

settingLinks.forEach(link => {
    link.addEventListener('click' , () => {
       settingLinks.forEach(link =>{
        
        link.classList.remove("border-b-2");
        link.classList.remove("border-black");
        link.classList.remove("font-extrabold");
         //hide link content
          document.querySelector(`.${link.id}`).classList.add("hidden");
        document.querySelector(`.${link.id}`).classList.remove("block");
      

        
       });
       link.classList.add("border-b-2");
        link.classList.add("border-black");
        link.classList.add("font-extrabold");
        //show link content
        document.querySelector(`.${link.id}`).classList.remove("hidden");
        document.querySelector(`.${link.id}`).classList.add("block");

    });
});

//text area style
let txtArea = document.getElementById("txt-area");
let boldSign = document.getElementById("bold-sign");
let italicSign = document.getElementById("italic-sign");

function textBold(){
    txtArea.classList.toggle("font-extrabold");
    txtArea.classList.toggle("text-md");

    boldSign.classList.toggle("bg-black/20")
}
function textItalic(){
    txtArea.classList.toggle("italic");
    italicSign.classList.toggle("bg-black/20");
};

//headline count
let headlineCount = document.getElementById("headline-count");

function headCount(n){
    let maxCount = 60 ;
    headlineCount.innerHTML = `${maxCount - n}`
};
// profile pic
let imagePlace = document.querySelector(".image-place");
let imgInput = document.querySelector(".img-src");
let urlPlace = document.querySelector(".img-url");
let uploadBtn = document.querySelector(".btn-upload");





imgInput.onchange = function(){
    urlPlace.innerHTML = imgInput.value;
    imagePlace.src = URL.createObjectURL(imgInput.files[0]);
   
}











