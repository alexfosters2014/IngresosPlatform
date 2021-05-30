function MostrarMsg(tipo, mensaje){
    if (tipo === "success") {
        Swal.fire(
            'Exitoso!',
            mensaje,
            'success'
        )
    }
        if (tipo === "error") {
            Swal.fire(
                'Error!',
                mensaje,
                'error'
            )
        }
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
    window.open(url, 'Visor', 'width=800,height=860,scrollbars=no,toolbar=no,location=no');
}


