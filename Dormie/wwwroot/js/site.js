window.sweetAlertHelper = {
    showSuccess: function (title, message) {
        return Swal.fire({
            icon: 'success',
            title: title,
            text: message,
            confirmButtonColor: '#3085d6'
        });
    },

    showError: function (title, message) {
        return Swal.fire({
            icon: 'error',
            title: title,
            text: message,
            confirmButtonColor: '#d33'
        });
    }

    //For Automatic navigation
    //showSuccess: function (title, message) {
    //    return Swal.fire({
    //        icon: 'success',
    //        title: title,
    //        text: message,
    //        timer: 2000,
    //        showConfirmButton: false
    //    });
    //}
};