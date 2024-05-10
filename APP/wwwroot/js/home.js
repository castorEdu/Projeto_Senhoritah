let productsAdded = [];
let contentDiv = document.getElementsByClassName('content-page')
function AddProduct(button) {
    let tr = button.parentElement
    const id = tr.children[0].value
    const Name = tr.children[1].textContent
    const Unidade = tr.children[2].children[0].value
    const Quantidade = tr.children[3].children[0].value
    let obj = {
        Id: id,
        Name: Name,
        Unidade: Unidade,
        Quantidade: Quantidade
    }
    productsAdded.push(obj)

    IncludeAddedProduct(obj)
}
function IncludeAddedProduct(item) {
    let div = document.createElement('div')
    div.classList.add('d-flex')

    let pNome = document.createElement('p')
    pNome.textContent = item.Name

    div.appendChild(pNome)

    let pUnidade = document.createElement('p')
    pUnidade.textContent = item.Unidade

    div.appendChild(pUnidade)

    let pQuantidade = document.createElement('p')
    pQuantidade.textContent = item.Quantidade

    div.appendChild(pQuantidade)

    contentDiv[0].appendChild(div)
}