
function clearRadio (){
    let radios = document.getElementsByClassName("form-check-input");
    for (i = 0; i < radios.length; i++) {
        radios[i].checked = false;
    }
}
