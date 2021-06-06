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


