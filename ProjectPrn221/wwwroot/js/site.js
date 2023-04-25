// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var myElement = document.getElementById('simple-bar');
new SimpleBar(myElement, { autoHide: true });
var myElement1 = document.getElementById('simple-bar1');
new SimpleBar(myElement1, { autoHide: true });




    function exportTableToExcel(tableID, filename = '') {
        var downloadLink;
    var dataType = 'application/vnd.ms-excel';
    var tableSelect = document.getElementById(tableID);
    var tableHTML = tableSelect.outerHTML.replace(/ /g, '%20');

    // Specify file name
    filename = filename ? filename + '.xls' : 'excel_data.xls';

    // Create download link element
    downloadLink = document.createElement("a");

    document.body.appendChild(downloadLink);

    if (navigator.msSaveOrOpenBlob) {
            var blob = new Blob(['\ufeff', tableHTML], {
        type: dataType
            });
    navigator.msSaveOrOpenBlob(blob, filename);
        } else {
        // Create a link to the file
        downloadLink.href = 'data:' + dataType + ', ' + tableHTML;

    // Setting the file name
    downloadLink.download = filename;

    //triggering the function
    downloadLink.click();
        }
    }
