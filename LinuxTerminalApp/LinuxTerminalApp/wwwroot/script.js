document.addEventListener('DOMContentLoaded', () => {
    const terminal = document.getElementById('terminal');
    const output = document.getElementById('output');
    const input = document.getElementById('input');
    
    input.addEventListener('keydown',async function (event){
        if (event.key === 'Enter') {

            var response = await fetch("commandline/"+input.value.trim());
            var data = await response.text();

            output.innerHTML += `<div>$ ${input.value}</div><div>${data}</div>`;
   
            input.value = '';
            terminal.scrollTop = terminal.scrollHeight;
            
        }
    });
});
