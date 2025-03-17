let sectionTable = document.querySelector(".section-table");
let lecTable = document.querySelector(".lec-table");
let popupSections = document.querySelector(".popup-sections");
let nameInput = document.querySelector(".name-typed");
let nameSection = document.querySelector(".section-name");
let popLec = document.querySelector(".pop-lec");
let tableRow = document.querySelector(".lec-body");
let editBtn = document.querySelector(".edit-btn");
let mood="add";
let index;
console.log(editBtn)

//resize table when click on aside
function tableFixed(){
    sectionTable.classList.toggle("w-2/3");
    lecTable.classList.toggle("w-2/3");
    
};

//edidth name of sections

function editName(){
    popupSections.classList.toggle("flex");
    popupSections.classList.toggle("hidden");
}

function cancelPop(){
    popupSections.classList.toggle("flex");
    popupSections.classList.toggle("hidden"); 
};

// function saveName(btn){
    
//     if(nameInput.value !=""){
         
//         nameSection.innerHTML=nameInput.value;
//         nameInput.value="";
//         cancelPop();
//     }else{
//         cancelPop();

//     }

// }

//lectures


function openLecPop(){
    popLec.classList.toggle("flex");
     popLec.classList.toggle("hidden");
}
function cancelLecPop(){
    popLec.classList.toggle("flex");
    popLec.classList.toggle("hidden"); 
};


// let nameIn = document.querySelector(".name-lec")
// let description = document.querySelector(".description")
// let video  = document.querySelector(".video ")
// let material = document.querySelector(".material");

// let dataStore;
// if(localStorage.lecData != null){
//     dataStore = JSON.parse(localStorage.lecData);
// }else{
//     dataStore=[];
// }
  
// function addLec(){
//     let dataObj={
//         lecName:nameIn.value,
//         description:description.value,
//         video:video.value,
//         material:material.value

//     }
//   if(mood === "add"){
//     dataStore.push(dataObj);
   
//   }else{
//     dataStore[index]=dataObj;
//     mood="add";
//     editBtn.innerHTML="add";


//   }
//   localStorage.setItem("lecData" , JSON.stringify(dataStore));
//     addData()
//     cancelLecPop()
   
// }  

// function addData(){
//     tableRow.innerHTML=``;
  

//     for(let i=0 ; i<dataStore.length ; i++){
//         tableRow.innerHTML += `
//         <tr>
//                 <td>${i+1}</td>
//                 <td>${dataStore[i].lecName}</td>
//                 <td>${dataStore[i].description}</td>
//                 <td>${dataStore[i].video}</td>
//                 <td>${dataStore[i].material}</td>
//                 <td>
//                   <div class="btns capitalize flex justify-center px-2 py-4  gap-2  ">
//                     <p href="" class="flex cursor-pointer items-center justify-center gap-2 bg-gray-500/80 hover:bg-gray-500 duration-700 text-white px-4 py-1 rounded-md">
//                         <!-- <ion-icon name="create" class="font-extrabold text-2xl"></ion-icon> -->
//                         <span onclick="edit(${i})">edit</span>
//                         <!-- <input type="submit" value="add" class="capitalize cursor-pointer" onclick="addLec()"> -->
//                     </p>
//                     <p href="" class="flex items-center justify-center cursor-pointer gap-2 bg-red-500/80 hover:bg-red-500 duration-700 text-white px-4 py-1 rounded-md">
//                         <!-- <ion-icon name="close-circle-outline" class="font-extrabold text-2xl"></ion-icon> -->
//                         <span onclick="deleteItem(${i})">delete</span>
//                     </p> 
//                   </div>

//                 </td>

//               </tr>
        
//         `
//     }
//     clearData();
    

// }
// //delete item
// function deleteItem(i){
//     dataStore.splice(i,1);
//     localStorage.setItem("lecData" , JSON.stringify(dataStore));
//     addData();

// };
// //clear data
// function clearData(){
//     nameIn.value = "" ;
//     description.value = "" ;
//     video.value = "" ;
//     material.value = "" ;
// }
//edit
function edit(i){
    openLecPop();
    nameIn.value = dataStore[i].lecName ;
    description.value =dataStore[i].description;
    video.value = dataStore[i].video ;
    material.value =dataStore[i].material ;
    editBtn.innerHTML="Edit";
    mood="edit";
    index=i;


    
}
// addData();





