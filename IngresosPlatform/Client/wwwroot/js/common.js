function DeshabilitarComponente(stringId, disabledBool) {
    document.getElementById(stringId).disabled = disabledBool;
}

function MsgSuccess(mensaje) {
    return new Promise(resolve => {
        Swal.fire({
            title: "Exitoso",
            text: mensaje,
            icon: 'success',
            confirmButtonColor: '#00b347',
            confirmButtonText: 'OK',
        }).then((result) => {
            resolve(result.isConfirmed);
        })

    });
}

function MsgError(mensaje) {
    return new Promise(resolve => {
    Swal.fire({
        title: "Error",
        text: mensaje,
        icon: 'error',
        confirmButtonColor: '#00b347',
        confirmButtonText: 'OK',
    }).then((result) => {
        resolve(result.isConfirmed);
    })

    });
}

function MsgWarning(mensaje) {
    return new Promise(resolve => {
    Swal.fire({
        title: "Advertencia",
        text: mensaje,
        icon: 'warning',
        confirmButtonColor: '#00b347',
        confirmButtonText: 'OK',
    }).then((result) => {
        resolve(result.isConfirmed);
    })

    });
    }

function ConfirmarOperacion (pregunta) {
    return new Promise(resolve => {
        Swal.fire({
            title: "Confirmar",
            text: pregunta,
            icon: 'question',
            showCancelButton: true,
            confirmButtonColor: '#00b347',
            cancelButtonColor: '#d33',
            confirmButtonText: 'SI',
            cancelButtonText: "NO"
        }).then((result) => {
                resolve(result.isConfirmed);
        })

    })
}

function OpenWindow(url) {
    window.open(url, 'Visor', 'width=800,height=860,scrollbars=no,toolbar=no,location=no, left='+ parseInt(window.innerWidth - 160)+'');
}

function OnInputText(titulo, mensaje) {
    return new Promise(resolve => {
        Swal.fire({
            title: titulo,
            text: mensaje,
            input: 'text',
            showCancelButton: true,
            confirmButtonColor: '#00b347',
            cancelButtonColor: '#d33',
            confirmButtonText: 'OK',
            cancelButtonColor: '#d33',
            cancelButtonText: "CANCELAR"
        }).then((result) => {
            if (!result.isConfirmed) {
                resolve("NOTCONFIRMED");
            }else if (result.value) {
                resolve(result.value);
            } else {
                resolve("");
            }
        })
    })
}



function OnInputTextPass() {
    (async () => {

        const { value: formValues } = await Swal.fire({
            title: 'Multiple inputs',
            html:
                '<input id="swal-input1" class="swal2-input">' +
                '<input id="swal-input2" class="swal2-input">',
            focusConfirm: false,
            preConfirm: () => {
                return [
                    document.getElementById('swal-input1').value,
                    document.getElementById('swal-input2').value
                ]
            }
        })

        if (formValues) {
            Swal.fire(JSON.stringify(formValues))
            return JSON.stringify(formValues);
        }

    })()
}

window.saveAsFile = function (fileName, byteBase64) {
    var link = this.document.createElement('a');
    link.download = fileName;
    link.href = "data:application/octet-stream;base64," + byteBase64;
    this.document.body.appendChild(link);
    link.click();
    this.document.body.removeChild(link);
}