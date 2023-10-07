if (localStorage.countNumbersValue === null) {
    localStorage.countNumbersValue = '2';
}

document.gcdCalculation.countNumbers.value = localStorage.countNumbersValue;

if (localStorage.countNumbersValue === '2') {
    chooseCountTwo();
} else if (localStorage.countNumbersValue === '3') {
    chooseCountThree();
} else if (localStorage.countNumbersValue === '4') {
    chooseCountMany();
}

window.onload = function () {
    let elCountTwo = document.getElementById('countTwo');
    let elCountThree = document.getElementById('countThree');
    let elCountMany = document.getElementById('countMany');

    elCountTwo.addEventListener('input', chooseCountTwo);
    elCountThree.addEventListener('input', chooseCountThree);
    elCountMany.addEventListener('input', chooseCountMany);
}

function chooseCountTwo() {
    localStorage.countNumbersValue = '2';
    clearNumbersThree();
    clearNumbersMany();
}

function chooseCountThree() {
    localStorage.countNumbersValue = '3';
    let elThird = document.getElementById('third');
    elThird.setAttribute('required', '');
    elThird.style.display = 'inline-block';
    let elNumbersThree = document.getElementById('numbersThree');
    elNumbersThree.style.display = 'inline-block';
    clearNumbersMany();
}

function chooseCountMany() {
    localStorage.countNumbersValue = '4';
    let elNumbersOther = document.getElementById('numbersOther');
    elNumbersOther.setAttribute('required', '');
    elNumbersOther.addEventListener('input', validateNumbersOther);
    document.gcdCalculation.addEventListener('submit', validateEachNumbersOther);
    let elNumbersMany = document.getElementById('numbersMany');
    elNumbersMany.style.display = 'block';
    clearNumbersThree();
}

function clearNumbersThree() {
    let elNumbersThree = document.getElementById('numbersThree');
    elNumbersThree.style.display = 'none';
    let elThird = document.getElementById('third');
    elThird.removeAttribute('required');
    elThird.style.display = 'none';
}

function clearNumbersMany() {
    document.gcdCalculation.removeEventListener('submit', validateEachNumbersOther);
    let elNumbersOther = document.getElementById('numbersOther');
    elNumbersOther.removeEventListener('input', validateNumbersOther);
    elNumbersOther.removeAttribute('required');
    let elNumbersMany = document.getElementById('numbersMany');
    elNumbersMany.style.display = 'none';
}

function validateNumbersOther() {
    let pattern = /^(\s*-?\d{1,10}\s+){0,2147483646}\s*-?\d{1,10}\s*$/;
    if (!pattern.test(this.value)) {
        this.setCustomValidity('Invalid characters.');
    } else {
        this.setCustomValidity('');
    }
}

function validateEachNumbersOther(e) {
    let elNumbersOther = document.getElementById('numbersOther');
    let numbersOtherArray = elNumbersOther.value.split(/[\r\n\s]+/);
    let count = numbersOtherArray.length;
    if (count > 2147483647) {
        let msg = 'Entered ' + count + ' values.';
        elNumbersOther.setCustomValidity(msg);
        alert(msg);
        e.preventDefault();
        return;
    }
    for (const element of numbersOtherArray) {
        let num = +element;
        if (num < -2147483647 || num > 2147483647) {
            let msg = 'The value ' + num + ' is not an integer in the allowed range.';
            elNumbersOther.setCustomValidity(msg);
            alert(msg);
            e.preventDefault();
            return;
        }
    }
    elNumbersOther.setCustomValidity('');
}