
// Write your Javascript code.

function showModal(url, title, target) {
    $(`#${target}`).modal('show');
    $(`#${target} .modal-body`).html('<div class="spinner-grow text-primary" style="margin-right: 1rem" role="status">\
                                              </div >' +
                                    '<div class="spinner-grow text-primary" style="margin-right: 1rem" role="status">\
                                              </div >' +
                                    '<div class="spinner-grow text-primary" role="status">\
                                            </div >');
    $.ajax({
        type: "GET",
        url: url,
        success: function(res) {
            $(`#${target} .modal-body`).html(res);
            $(`#${target} .modal-title`).html(title);
        }
    });
}

function submitAjaxForm(form, target) {
    try {
        $.ajax({
            type: "POST",
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.success) {
                    $(`#${target}`).html(res.html);
                    $(`#${target} .modal-body`).html("");
                    $(`#${target}`).modal('hide'); 
                } else {
                    $(`#${target} .modal-body`).html(res.html);
                }
            },
            error: function(err) {
                console.log(e);
            }
        });
    } catch (e) {
        console.log(e);
    }
    return false;
}