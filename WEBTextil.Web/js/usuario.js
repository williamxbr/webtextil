var usuarioControle = {

    DeletaUsuario: function (id, item) {
        bootbox.confirm({
            title: "EXCLUIR USUARIO?",
            message: "VOCÊ TEM CERTEZA QUE GOSTARIA DE DELETAR O USUARIO? ESSA OPÇÃO NÃO PODERÁ SER REVERTIDA!",
            buttons: {
                cancel: {
                    label: '<i class="fa fa-times"></i> CANCELAR'
                },
                confirm: {
                    label: '<i class="fa fa-check"></i> CONFIRMAR'
                }
            },
            callback: function (result) {
                if (result) {
                    $.ajax({
                        url: urlAjax.usuario.DeletarUsuario,
                        type: "POST",
                        data: { id },
                        success: function (retorno) {
                            bootbox.alert(retorno.mensagem);
                            if (retorno.sucesso) {
                                $("#listaUsuario").jsGrid("deleteItem", item);
                            }
                        },
                        error: function (mensagem) {
                            bootbox.alert(mensagem);
                        }
                    });
                }
            }
        });
    },

    AtivarUsuario: function (ativa, id, element) {
        console.log(element);
        var ativaOudeativa = ativa === true ? "ATIVAR USUARIO" : "DESATIVAR USUARIO?"
        bootbox.confirm({
            title: ativaOudeativa,
            message: "VOCÊ TEM CERTEZA QUE GOSTARIA DE " + ativaOudeativa + "? ESSA OPÇÃO NÃO PODERÁ SER REVERTIDA!",
            buttons: {
                cancel: {
                    label: '<i class="fa fa-times"></i> CANCELAR'
                },
                confirm: {
                    label: '<i class="fa fa-check"></i> CONFIRMAR'
                }
            },
            callback: function (result) {
                if (result) {
                    $.ajax({
                        url: $("#URLUsuarioAtivarrUsuario").val(),
                        type: "POST",
                        data: { id, ativa },
                        success: function () {
                            if (ativa) {
                                $("#ativausuario" + id).val("DESATIVAR");
                                $("#ativausuario" + id).attr("onclick", "AtivarUsuario(false, " + id + ");");
                            } else {
                                $("#ativausuario" + id).val("ATIVAR");
                                $("#ativausuario" + id).attr("onclick", "AtivarUsuario(true, " + id + ");");
                            }
                            bootbox.alert("OPERACAO EFETUADA COM SUCESSO!");
                        },
                        error: function (mensagem) {
                            alert(mensagem);
                        }
                    });
                }
            }
        });
    },

    Listagem: function () {
        $.getJSON(urlAjax.usuario.ListarUsuario, function (ListaUsuario) {
            $("#listaUsuario").jsGrid({
                width: "100%",
                height: "auto",

                noDataContent: "Não encontrado",

                autoload: true,
                sorting: true,
                filtering: false,
                paging: true,
                pageSize: 10,
                confirmDeleting: false,

                controller: {
                    loadData: function (filter) {
                        return (!filter.UsuarioId &&
                            !filter.Login &&
                            !filter.Nome &&
                            !filter.Ativo)
                            ? ListaUsuario
                            : $.grep(ListaUsuario, function (item) {

                                return (filter.UsuarioId !== undefined ? item.UsuarioId === filter.UsuarioId : true) &&
                                    (filter.Login.length > 0 && item.Login !== null ? item.Login.indexOf(filter.Login.toUpperCase()) > -1 : true) &&
                                    (filter.Nome.length > 0 && item.Nome !== null ? item.Nome.indexOf(filter.Nome.toUpperCase()) > -1 : true) &&
                                    (filter.Ativo !== undefined ? item.Ativo === filter.Ativo : true)
                            });
                    }
                },

                fields: [
                    {
                        title: "ID", name: "UsuarioId", type: "number", width: 30,
                        itemTemplate: function (value, item) {
                            return $('<a/>').attr('href', urlAjax.usuario.Editar + '/' + value).html(value)
                        }
                    },
                    {
                        title: "Imagem", name: "Imagem", type: "text",
                        itemTemplate: function (value, item) {
                            if (value) {
                                var $photo = $("<div>").append($("<img>").attr("src", value).width("60"));
                                return $photo;
                            } else {
                                return "";
                            }
                        }
                    },
                    { title: "Login", name: "Login", type: "text" },
                    { title: "NOME", name: "Nome", type: "text" },
                    { title: "Admin", name: "Admin", type: "checkbox", sorting: false, width: 15 },
                    { title: "Ativo", name: "Ativo", type: "checkbox", sorting: false, width: 15 },
                    {
                        type: "control", editButton: false, deleteButton: false, width: 15, align: "center",
                        headerTemplate: function () {
                            var $iconPLus = $("<i>").attr({ class: "fa fa-plus-circle" });

                            return $("<button>")
                                .attr({ role: "button" })
                                .attr({ class: "btn btn-success btn-sm" })
                                .attr({ title: jsGrid.fields.control.prototype.inseretButtonTooltip })
                                .on("click", function () {
                                    window.location.href = urlAjax.usuario.Novo;
                                }).append($iconPLus);
                        },
                        itemTemplate: function (value, item) {
                            var $iconTrash = $("<i>").attr({ class: "fa fa-trash" });

                            var $customDeleteButton = $("<button>")
                                .attr({ class: "btn btn-danger btn-sm" })
                                .attr({ role: "button" })
                                .attr({ title: jsGrid.fields.control.prototype.deleteButtonTooltip })
                                .attr({ id: "btn-delete-" + item.UsuarioId })
                                .click(function (e) {
                                    usuarioControle.DeletaUsuario(item.UsuarioId, item);
                                    e.stopPropagation();
                                })
                                .append($iconTrash);

                            return $("<div>").attr({ class: "btn-toolbar" })
                                .append($customDeleteButton);
                        }
                    }

                ]
            });
        });
    },

    CarregarUsuario: function (id) {
        $.getJSON(urlAjax.usuario.BuscarUsuario, { id }, function (usuario) {

            $("#UsuarioId").val(usuario.UsuarioId);
            $("#Login").val(usuario.Login);
            $("#Senha").val(usuario.Senha);
            $("#Nome").val(usuario.Nome);
            $("#Email").val(usuario.Email);
            $("#Ativo")[0].checked = usuario.Ativo;
            $("#Admin")[0].checked = usuario.Admin;
            $("#Permissao").val(usuario.Permissao);
            $("#Imagem").val(usuario.Imagem);

        });
    },

    GravarUsuario: function (redirect) {


        var usuario = {
            UsuarioId: $("#UsuarioId").val(),
            Login: $("#Login").val(),
            Senha: $("#Senha").val(),
            Nome: $("#Nome").val(),
            Email: $("#Email").val(),
            Ativo: $("#Ativo")[0].checked,
            Admin: $("#Admin")[0].checked,
            Permissao: $("#Permissao").val(),
            Imagem: $("#Imagem").val()

        }

        bootbox.confirm("Gravar Usuario?",
            function (result) {
                if (result) {
                    $.ajax({
                        url: urlAjax.usuario.GravarUsuario,
                        type: 'POST',
                        data: { usuario: usuario },
                        success: function (response) {
                            if (response.sucesso) {
                                if (redirect) {
                                    bootbox.alert('Usuario gravado com sucesso');
                                    window.location.href = urlAjax.filial.Lista;
                                } else {
                                    toastr.success("Usuario gravada com sucesso");
                                }
                            }
                            else {
                                toastr.error(response.mensagem);
                            }
                        }
                    });
                }
            });
    }
};