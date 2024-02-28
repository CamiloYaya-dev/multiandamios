function autoResize(t, maxL, e) {
    var nav4 = window.Event ? true : false;
    var key = nav4 ? e.keyCode : e.which;
    if (key == null || key == 8 || (key>=37 && key<=40) || key==46 || t.value.length <= maxL) {
        a = t.value.split('\n');
        b = 0;
        for (x = 0; x < a.length; x++) {
            if (a[x].length >= t.cols) b += Math.floor(a[x].length / t.cols);
        }
        b += a.length;
        if (b > t.rows || b < t.rows) t.rows = (b < t.initialRows ? t.initialRows : b);
        return true;
    } else {
    return false;
    }
}
function getAbsoluteElementPosition(element) {
    if (typeof element == "string")
        element = document.getElementById(element)

    if (!element) return { top: 0, left: 0 };

    var y = 0;
    var x = 0;
    while (element.offsetParent) {
        x += element.offsetLeft;
        y += element.offsetTop;
        
        element = element.offsetParent;
    }
    return { top: y, left: x};
}

function getY(oElement) {
    var iReturnValue = 0;
    while (oElement != null) {
        iReturnValue += oElement.offsetTop;
        oElement = oElement.offsetParent;
    }
    return iReturnValue;
}

function AcceptPje(evt) {
    var nav4 = window.Event ? true : false;
    var key = nav4 ? evt.keyCode : evt.which;
    return (key <= 13 || (key >= 48 && key <= 57) || (key >= 96 && key <= 105) || key == 44 || (key >= 37 && key <= 40) || key == 188 || key == 190 || key == 110);
}
function FormateaNum(Campo) {
    var NumeroTest = 1.1;
    var separador_miles = "";
    var separador_decimal = NumeroTest.toLocaleString().substring(1, 2);
    var Texto = "";
    
    switch (separador_decimal) {
        case ".": separador_miles = ","; break;
        case ",": separador_miles = "."; break;
    }
    Texto = Campo.value.toString().replace(separador_miles, "");
    

    if (separador_miles) {
        // Añadimos los separadores de miles
        var miles = new RegExp("(-?[0-9]+)([0-9]{3})");
        NumFormateado = Texto;
        while (miles.test(NumFormateado)) {
            NumFormateado = NumFormateado.replace(miles, "$1" + separador_miles + "$2");
        }
    }

    Campo.value = NumFormateado;
}

function Show_Motivo_Inactivacion(Inactivo) {
    if (Actualizacion == "Si") {
        var Modal = document.getElementById('Oscuro');
        Modal.style.height = document.body.clientHeight + "px";
        Modal.style.display = 'block';
    }
    
}
function Hide_Motivo_Inactivacion() {
    var Motivos = document.getElementById('divMotivoInactividad');
    Motivos.style.display = 'none';
    var Modal = document.getElementById('Oscuro');
    Modal.style.display = 'none';    

}
function OcultaTextoDescriptivo(Campo, Texto) {
    if(Texto == Campo.value){
        Campo.value = "";
        Campo.style.color = "Black";
    }
}
function MuestraTextoDescriptivo(Campo, Texto) {
    if (Campo.value == "") 
    {
        Campo.value = Texto;
        Campo.style.color = "Gray";
    }
}


function carga() {
    posicion = 0;

    // IE
    if (navigator.userAgent.indexOf("MSIE") >= 0) navegador = 0;
    // Otros
    else navegador = 1;
}

function evitaEventos(event) {
    // Funcion que evita que se ejecuten eventos adicionales
    if (navegador == 0) {
        window.event.cancelBubble = true;
        window.event.returnValue = false;
    }
    if (navegador == 1) event.preventDefault();
}

function comienzoMovimiento(event, id) {
    elMovimiento = document.getElementById(id);

    // Obtengo la posicion del cursor
    if (navegador == 0) {
        cursorComienzoX = window.event.clientX + document.documentElement.scrollLeft + document.body.scrollLeft;
        cursorComienzoY = window.event.clientY + document.documentElement.scrollTop + document.body.scrollTop;

        document.attachEvent("onmousemove", enMovimiento);
        document.attachEvent("onmouseup", finMovimiento);
    }
    if (navegador == 1) {
        cursorComienzoX = event.clientX + window.scrollX;
        cursorComienzoY = event.clientY + window.scrollY;

        document.addEventListener("mousemove", enMovimiento, true);
        document.addEventListener("mouseup", finMovimiento, true);
    }

    elComienzoX = parseInt(elMovimiento.style.left);
    elComienzoY = parseInt(elMovimiento.style.top);
    // Actualizo el posicion del elemento
    elMovimiento.style.zIndex = 1001;

    //evitaEventos(event);
}

function enMovimiento(event) {
    var xActual, yActual;
    if (navegador == 0) {
        xActual = window.event.clientX + document.documentElement.scrollLeft + document.body.scrollLeft;
        yActual = window.event.clientY + document.documentElement.scrollTop + document.body.scrollTop;
    }
    if (navegador == 1) {
        xActual = event.clientX + window.scrollX;
        yActual = event.clientY + window.scrollY;
    }

    elMovimiento.style.left = (elComienzoX + xActual - cursorComienzoX) + "px";
    elMovimiento.style.top = (elComienzoY + yActual - cursorComienzoY) + "px";

    evitaEventos(event);
}

function finMovimiento(event) {
    if (navegador == 0) {
        document.detachEvent("onmousemove", enMovimiento);
        document.detachEvent("onmouseup", finMovimiento);
    }
    if (navegador == 1) {
        document.removeEventListener("mousemove", enMovimiento, true);
        document.removeEventListener("mouseup", finMovimiento, true);
    }
}

window.onload = carga;
