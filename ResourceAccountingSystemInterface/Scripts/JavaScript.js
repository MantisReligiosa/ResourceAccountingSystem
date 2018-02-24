function addHouseSuccess(result) {
    if (result.success) {
        $('#houseAddress').val('');
        alert(result.message);
        $('#housesList').append(
            $('<li>').addClass('list-group-item').attr('id', `house${result.data.Id}`).css('height', '53px').append(
                $('<label>').text(result.data.Address)).append(
                $('<button>').addClass('btn btn-danger glyphicon glyphicon-remove')
                    .attr('id', `delete${result.data.Id}`)
                    .css('float', 'right')
                    .on('click', function () { deleteHouse(result.data.Id); return false; })).append(
                $('<button>').addClass('btn btn-warning glyphicon glyphicon-edit')
                    .attr('id', `edit${result.data.Id}`)
                    .css('float', 'right')
                    .css('margin-right', '5px')
                    .on('click', function () { editHouse(result.data.Id); return false; })
                ));
    }
}

function deleteHouse(id) {
    $.ajax({
        url: deleteHouseUrl,
        type: 'POST',
        data: { houseId: id },
        success: function (res) {
            if (res.success) {
                $('#house' + id).remove();
                alert(res.message);
            }
            else {
                alert(res.message);
            }
        }
    });
}