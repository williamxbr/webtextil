


function DataDeHoje(elemento) {
    if (elemento.value.length == 0) {
        elemento.value = new Date().toLocaleDateString();
    }
}

function DataJson(data) {
    if (data.indexOf('/Date') >= 0) {
        return new Date(parseInt(data.slice(6, 20))).toLocaleString('pt-br');
    }
    return data;
}

function cnpj(v) {
    v = v.replace(/\D/g, "")                           //Remove tudo o que não é dígito
    v = v.replace(/^(\d{2})(\d)/, "$1.$2")             //Coloca ponto entre o segundo e o terceiro dígitos
    v = v.replace(/^(\d{2})\.(\d{3})(\d)/, "$1.$2.$3") //Coloca ponto entre o quinto e o sexto dígitos
    v = v.replace(/\.(\d{3})(\d)/, ".$1/$2")           //Coloca uma barra entre o oitavo e o nono dígitos
    v = v.replace(/(\d{4})(\d)/, "$1-$2")              //Coloca um hífen depois do bloco de quatro dígitos
    return v
}


function BuscarCep(cep) {
    $.getJSON("https://viacep.com.br/ws/" + cep + "/json/", function (resposta) {
        return resposta;
    });
}


function DataJson2(data) {
    let regex = /Date\((\d+)([-+]\d{4})?\)/;
    if (regex.exec(data) === null) return null;
    let match = regex.exec(data).slice(1, 3);
    return new Date(parseInt(match[0])).toLocaleDateString();
}

function tableToDataTable(element) {
    // Setup - add a text input to each footer cell
    $(element + ' thead tr').clone(true).appendTo(element + ' thead');
    $(element + ' thead tr:eq(1) th').each(function (i) {
        var title = $(this).text();
        $(this).html('<input type="text" class="form-control form-control-sm" placeholder="Pesquise ' + title.trim() + '" />');

        $('input', this).on('keyup change', function () {
            if (table.column(i).search() !== this.value) {
                table
                    .column(i)
                    .search(this.value)
                    .draw();
            }
        });
    });

    var table = $(element).DataTable({
        searching: true,
        orderCellsTop: true,
        fixedHeader: true,
        dom: 'Bfrtip',
        buttons: ['excel', 'pdf', 'print'],
        "language": {
            "sEmptyTable": "NENHUM REGISTRO ENCONTRADO",
            "sInfo": "_START_ - _END_ de _TOTAL_ REGISTROS",
            "sInfoEmpty": "MOSTRANDO 0 ATÉ 0 de 0 REGISTROS",
            "sInfoFiltered": "(FILTRADOS DE _MAX_ REGISTROS)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "_MENU_ RESULTADOS POR PÁGINA",
            "sLoadingRecords": "CARREGANDO...",
            "sProcessing": "PROCESSANDO...",
            "sZeroRecords": "NENHUM REGISTRO ENCONTRADO",
            "sSearch": "PESQUISAR",
            "oPaginate": {
                "sNext": "PRÓXIMO",
                "sPrevious": "ANTERIOR",
                "sFirst": "PRIMEIRO",
                "sLast": "ÚLTIMO"
            },
            "oAria": {
                "sSortAscending": ": ORDENAR COLUNAS DE FORMA ASCENDENTE",
                "sSortDescending": ": ORDENAR COLUNAS DE FORMA DESCENDENTE"
            }
        }
    });
}


function JSONToCSVConvertor(JSONData, ReportTitle, ShowLabel) {
    //If JSONData is not an object then JSON.parse will parse the JSON string in an Object
    var arrData = typeof JSONData != 'object' ? JSON.parse(JSONData) : JSONData;

    var CSV = '';
    //Set Report title in first row or line

    CSV += ReportTitle + '\r\n\n';

    //This condition will generate the Label/Header
    if (ShowLabel) {
        var row = "";

        //This loop will extract the label from 1st index of on array
        for (var index in arrData[0]) {

            //Now convert each value to string and comma-seprated
            row += index + ',';
        }

        row = row.slice(0, -1);

        //append Label row with line break
        CSV += row + '\r\n';
    }

    //1st loop is to extract each row
    for (var i = 0; i < arrData.length; i++) {
        var row = "";

        //2nd loop will extract each column and convert it in string comma-seprated
        for (var index in arrData[i]) {
            row += '"' + arrData[i][index] + '",';
        }

        row.slice(0, row.length - 1);

        //add a line break after each row
        CSV += row + '\r\n';
    }

    if (CSV == '') {
        alert("Invalid data");
        return;
    }

    //Generate a file name
    var fileName = "MyReport_";
    //this will remove the blank-spaces from the title and replace it with an underscore
    fileName += ReportTitle.replace(/ /g, "_");

    //Initialize file format you want csv or xls
    var uri = 'data:text/csv;charset=utf-8,' + escape(CSV);

    // Now the little tricky part.
    // you can use either>> window.open(uri);
    // but this will not work in some browsers
    // or you will not get the correct file extension    

    //this trick will generate a temp <a /> tag
    var link = document.createElement("a");
    link.href = uri;

    //set the visibility hidden so it will not effect on your web-layout
    link.style = "visibility:hidden";
    link.download = fileName + ".csv";

    //this part will append the anchor tag and remove it after automatic click
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

toastr.options = {
    "closeButton": true,
    "debug": false,
    "newestOnTop": false,
    "progressBar": false,
    "positionClass": "toast-top-right",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}

jsGrid.locale("pt-br");

//$.fn.datepicker.defaults.format = "dd/mm/yyyy";
$.fn.select2.defaults.set( "theme", "bootstrap" );



