let h=document.getElementById("height");
let w=document.getElementById("weight");
let Bm=document.getElementById("bmi");
let bt=document.getElementById("bt");
let para=document.getElementById("des");

bt.onclick=function (){
    let he=parseFloat(h.value)/100;
    let we=parseFloat(w.value)
let b=(we/(he*he)).toFixed(1);
Bm.innerHTML="BMI :"+b;
if(b<18.5){
para.innerHTML="Underweight";
des.style.color = "red";
}else if(b>=18.5 && b<=24.9){
para.innerHTML="Healthy Weight";
des.style.color = "green";
}else if(b>=25 && b<=29.9){
    para.innerHTML="Overweight";
    des.style.color = "yellow";
}else{
    para.innerHTML="Obesity";
    des.style.color = "red";
}
}

