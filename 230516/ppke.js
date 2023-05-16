//!! 배열 인덱스 forloop를 등분해서 돌리는 예제
//  나중에 공식이 쓸만할지는 모르겠다 백업용

const fn_gllr = function(l, j, i) {
    if (typeof l !== 'number') return i;
    if (typeof j !== 'number') return i;
    if (typeof i !== 'number') return i;
    if (l < j) return i;

    let m = Math.ceil(l / j);
    let r = Math.floor(i / j);
    let h = i % j;
    let k = (m * h) + r;
    k = Math.round(k);
    // console.log(k);
    return k;
};




let arr = new Array(30000);
arr.fill('박종명');
arr[145] = '정희범';

let l = arr.length;
let j = 30000 / 145;
for (let i = 0; i < l; ++i) {
    let k = fn_gllr(l, j, i);
    let v = arr[k];
    if (v === '정희범') {
        console.log(`찾았음: ${k}, ${i}`);
        break;
    }
}

console.log('>>>>>>>>>>');




// let l = 10;
// let j = 2;
// for (let i = 0; i < l; ++i) {
//     let k = fn_gllr(l, j, i);
//     console.log(k);
// }

// console.log('>>>>>>>>>>');

/*
#9/3 >>>
036
147
258


#10/2 >>>
05
16
27
38
49

*/


