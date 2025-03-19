const inp = document.getElementById("profile-img-inp");
const img = document.getElementById("profile-img");

inp.onchange = (ev) => {
  img.src = URL.createObjectURL(ev.target.files[0]);
  //   img.onload = function () {
  //     URL.revokeObjectURL(output.src);
  //   };
};
