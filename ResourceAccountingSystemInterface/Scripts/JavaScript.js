/*eslint eqeqeq: ["error", "smart"]*/
$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
    $('#getMaxConsumption').click(function () {
        $.ajax({
            url: getMaxConsumption,
            type: 'POST',
            success: function (res) {
                if (res.success) {
                    alert('Дом по адресу ' + res.data.Address + '\r\nСчётчик номер ' + res.data.Meter.Serial + '\r\nПоказания: ' + res.data.Meter.ReadingValue);
                }
                else {
                    alert(res.message);
                }
            }
        });
    });
    $('#getMinConsumption').click(function () {
        $.ajax({
            url: getMinConsumption,
            type: 'POST',
            success: function (res) {
                if (res.success) {
                    alert('Дом по адресу ' + res.data.Address + '\r\nСчётчик номер ' + res.data.Meter.Serial + '\r\nПоказания: ' + res.data.Meter.ReadingValue);
                }
                else {
                    alert(res.message);
                }
            }
        });
    });
});

function addHouseSuccess(result) {
    if (result.success) {
        $('#houseAddress').val('');
        alert(result.message);
        $('#housesList').append(
            $('<li>').addClass('list-group-item').attr('id', `house${result.data.Id}`).css('height', '53px').append(
                $('<label>').text(result.data.Address)).append(
                $('<button>').addClass('btn btn-danger glyphicon glyphicon-remove')
                    .attr('id', `delete${result.data.Id}`)
                    .attr('data-toggle', 'tooltip')
                    .attr('title', 'Удалить')
                    .attr('data-placement', 'left')
                    .css('float', 'right')
                    .attr('onclick', "deleteHouse(" + result.data.Id + ");return false;")).append(
                $('<button>').addClass('btn btn-warning glyphicon glyphicon-dashboard')
                    .attr('id', `edit${result.data.Id}`)
                    .css('float', 'right')
                    .css('margin-right', '5px')
                    .attr('onclick', "editHouse(" + result.data.Id + ");return false;")).append(
                $('<button>').addClass('btn btn-success glyphicon glyphicon-pencil')
                    .attr('id', `meter_ofHouse${result.data.Id}`)
                    .css('float', 'right')
                    .css('margin-right', '5px')
                        .attr('onclick', "updateMeterValue('');return false;")
                ));
    }
    else {
        alert(result.message);
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

function editHouse(id) {
    var address = $('#house' + id + '>label').text();
    $('#houseIdUpdate').val(id);
    $('#houseAddressUpdate').val(address);
    $('#meterSerialUpdate').attr('disabled', false);
    $('#meterSerialSubmit').attr('disabled', false);
}

function updateHouseMeterSuccess(result) {
    if (result.success) {
        var houseId = $('#houseIdUpdate').val();
        var serial = result.data;
        $('#meter_ofHouse' + houseId).attr('onclick', "updateMeterValue('" + serial + "');return false;");
        $('#houseIdUpdate').val('');
        $('#houseAddressUpdate').val('');
        $('#meterSerialUpdate').val('');
        $('#meterSerialUpdate').attr('disabled', true);
        $('#meterSerialSubmit').attr('disabled', true);
        alert(result.message);
    }
    else {
        alert(result.message);
    }
}

function updateMeterValue(serial) {
    if (serial == '') {
        alert('Нет информации о счётчике!');
        return;
    }
    $('#meterSerialUpdateVal').val(serial);
    $('#meterValuelUpdate').val(0);
}

function updateMeterReadingValueSuccess(result) {
    if (result.success) {
        $('#meterSerialUpdateVal').val('');
        $('#meterValueUpdate').val('');
        alert(result.message);
    }
    else {
        alert(result.message);
    }
}