using System;
using System.Collections.Generic;

namespace WEBTextil.Web.Helpers
{
    public static class CodeGenerationHelper
    {
        public static List<CodigoLinha> ListaMetodos(Type controle)
        {
            var lista = new List<CodigoLinha>();
            var controleName = controle.Name.Replace("Controller", "");

            var titulo = new CodigoLinha() { Descricao = $"<!--{controleName}-->" };
            lista.Add(titulo);

            foreach (var method in controle.GetMethods())
            {

                if (method.ReturnType.Name == "ActionResult" || method.ReturnType.Name == "JsonResult")
                {
                    var codigoLinha = new CodigoLinha() { Descricao = $"@Html.Hidden(\"URL{controleName}{method.Name}\", Url.Action(\"{method.Name}\", \"{controleName}\"))" };
                    lista.Add(codigoLinha);
                }

            }

            return lista;
        }

        public static List<CodigoLinha> ListaUrlAjax(Type controle)
        {
            var lista = new List<CodigoLinha>();
            var controleName = controle.Name.Replace("Controller", "");

            var controleNameUrl = new CodigoLinha() { Descricao = $"{controleName.ToLower()}: " + "{" };
            lista.Add(controleNameUrl);

            foreach (var method in controle.GetMethods())
            {

                if (method.ReturnType.Name == "ActionResult" || method.ReturnType.Name == "JsonResult")
                {
                    var codigoLinha = new CodigoLinha() { Descricao = $"{method.Name}: $('#URL{controleName}{method.Name}').val()," };
                    lista.Add(codigoLinha);
                }

            }

            lista.Add(new CodigoLinha() { Descricao = "}," });

            return lista;
        }

        public static List<CodigoLinha> ListaDeletarJs(Type model)
        {

            var lista = new List<CodigoLinha>();

            var lowerName = model.Name.ToLower();
            var upperName = model.Name.ToUpper();

            string delete = @"

        /// - Copiar para o arquivo [lowerName].js e inserir dentro do objeto [lowerName]Controle
        /// Inicio do metodo Deletar[model] 

        Deletar[model]: function (id, item) {
        bootbox.confirm({
            title: ""EXCLUIR [upperName]?"",
            message: ""VOCÊ TEM CERTEZA QUE GOSTARIA DE DELETAR O [upperName]? ESSA OPÇÃO NÃO PODERÁ SER REVERTIDA!"",
            buttons:
            {
            cancel:
                {
                label: '<i class=""fa fa-times""></i> CANCELAR'
                },
                confirm:
                {
                label: '<i class=""fa fa-check""></i> CONFIRMAR'
                }
            },
            callback: function(result) {
                if (result)
                {
                    $.ajax({
                    url: urlAjax.[lowerName].Deletar[model],
                        type: ""POST"",
                        data: { id },
                        success: function(retorno) {
                            bootbox.alert(retorno.mensagem);
                            if (retorno.sucesso)
                            {
                                $(""#lista[model]"").jsGrid(""deleteItem"", item);
                            }
                        },
                        error: function(mensagem) {
                            bootbox.alert(mensagem);
                        }
                    });
                }
            }
        });
      },

        /// Fim do metodo Deletar[model] -------------------------------------------------------------------------------------

            ";

            lista.Add(new CodigoLinha() { Descricao = delete.Replace("[model]", model.Name).Replace("[lowerName]", lowerName).Replace("[upperName]", upperName) });

            return lista;

        }

        public static List<CodigoLinha> ListaListagemJsGrid(Type model)
        {
            var lista = new List<CodigoLinha>();
            var lowerName = model.Name.ToLower();
            var upperName = model.Name.ToUpper();

            var filtros = new List<string>();
            var retorno = new List<string>();
            var campos = new List<string>();

            foreach (var campo in model.GetProperties())
            {
                filtros.Add($"!filter.{campo.Name}");

                switch (Type.GetTypeCode(campo.PropertyType))
                {
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                    case TypeCode.Double:
                    case TypeCode.Decimal:
                        retorno.Add($"(filter.{campo.Name} !== undefined ? item.{campo.Name} === filter.{campo.Name} : true)");
                        if (campo.Name == $"{model.Name}Id")
                        {
                            campos.Add(@"{ title: ""[campoName]"", name: ""[campoName]"", type: ""number"", width: 10,
                                         itemTemplate: function (value, item) {
                                                        return $('<a/>').attr('href', urlAjax.[lowerName].Editar + '/' + value).html(value)
                                          }
                            }".Replace("[campoName]", campo.Name).Replace("[lowerName]", lowerName));
                        }
                        else
                        {
                            campos.Add(@"{ title: ""[campoName]"", name: ""[campoName]"", type: ""number"", width: 20 }".Replace("[campoName]", campo.Name));
                        }
                        break;
                    case TypeCode.DateTime:
                        retorno.Add($"(filter.{campo.Name}.length > 0 && item.{campo.Name} !== null ? item.{campo.Name}.indexOf(filter.{campo.Name}.toUpperCase()) > -1 : true)");
                        campos.Add(@"{ title: ""[campoName]"", name: ""[campoName]"", type: ""text"", width: 50 }".Replace("[campoName]", campo.Name));
                        break;
                    case TypeCode.String:
                        retorno.Add($"(filter.{campo.Name}.length > 0 && item.{campo.Name} !== null ? item.{campo.Name}.indexOf(filter.{campo.Name}.toUpperCase()) > -1 : true)");
                        campos.Add(@"{ title: ""[campoName]"", name: ""[campoName]"", type: ""text"", width: 50 }".Replace("[campoName]", campo.Name));
                        break;
                }

            }

            var listagem = @"

        /// - Copiar para o arquivo [lowerName].js e inserir dentro do objeto [lowerName]Controle
        /// Inicio do metodo Listagem[model]

                                Listagem[model]: function () {
                                    $.getJSON(urlAjax.[lowerName].Listar[model], function (lista) {
                                        $(""#lista[model]"").jsGrid({
                                            width: ""100%"",
                                            height: ""auto"",
                                            noDataContent: ""Não encontrado"",
                                            autoload: true,
                                            sorting: true,
                                            filtering: true,
                                            paging: true,
                                            pageSize: 10,
                                            controller:     {
                                                    loadData: function(filter) {
                                                              return ([checafilter])
                                                                      ? lista
                                                                      : $.grep(lista, function(item) {
                                                                       return [retornafilter];
                                                                     });
                                                     }
                                                },
                                            fields:
                                                [[campos],
                                                {
                                                    type: ""control"", editButton: false, deleteButton: false, width: 5,
                                                    itemTemplate: function(value, item) {
                                            var $iconTrash = $(""<i>"").attr({ class: ""fa fa-trash"" });

                                                        var $customDeleteButton = $(""<button>"")
                                                            .attr({ class: ""btn btn-danger btn-sm"" })
                                                            .attr({ role: ""button"" })
                                                            .attr({ title: jsGrid.fields.control.prototype.deleteButtonTooltip })
                                                            .attr({ id: ""btn-delete-"" + item.[model]Id })
                                                            .click(function(e) {
                                                                    [lowerName].Deletar[model](item.[model]Id, item);
                                                                    e.stopPropagation();
                                                            })
                                                            .append($iconTrash);

                                                            return $(""<div>"").attr({ class: ""btn-toolbar"" })
                                                            .append($customDeleteButton);
                                                    }
                                                }
                                        ]
                                        });
                                    });
                                },

        /// Fim do metodo Listagem[model] -------------------------------------------------------------------------

";
            lista.Add(new CodigoLinha()
            {
                Descricao = listagem.Replace("[model]", model.Name)
                                    .Replace("[lowerName]", lowerName)
                                    .Replace("[upperName]", upperName)
                                    .Replace("[checafilter]", string.Join(" && \r\n", filtros))
                                    .Replace("[retornafilter]", string.Join(" && \r\n", retorno))
                                    .Replace("[campos]", string.Join(", \r\n", campos))
            });

            return lista;

        }

        public static List<CodigoLinha> ListaGravarJs(Type model)
        {

            var lista = new List<CodigoLinha>();
            var lowerName = model.Name.ToLower();
            var upperName = model.Name.ToUpper();

            var filtros = new List<string>();
            var retorno = new List<string>();
            var campos = new List<string>();

            foreach (var campo in model.GetProperties())
            {
                campos.Add(@"[campoNome]: $(""#[campoNome]"").val()".Replace("[campoNome]", campo.Name));

            }


            var gravar = @"

        /// - Copiar para o arquivo [lowerName].js e inserir dentro do objeto [lowerName]Controle
        /// Inicio do metodo Gravar[model]


    Gravar[model]: function () {

        var [lowerName] = {[campos]}

        bootbox.confirm(""Gravar [model]?"",
            function(result)
        {
            if (result)
            {
                    $.ajax({
                url: urlAjax.[lowerName].Gravar[model],
                        type: 'POST',
                        data: { [lowerName]: [lowerName] },
                        success: function(response) {
                        if (response.sucesso)
                        {
                            bootbox.alert('[model] gravado com sucesso');
                            window.location.href = urlAjax.[lowerName].Lista;
                        }
                        else
                        {
                            toastr.error(response.mensagem);
                        }
                    }
                });
            }
        });
    },

        /// Fim do metodo Gravar[model] ----------------------------------------------------------------


";

            lista.Add(new CodigoLinha()
            {
                Descricao = gravar.Replace("[model]", model.Name)
                        .Replace("[lowerName]", lowerName)
                        .Replace("[upperName]", upperName)
                        .Replace("[campos]", string.Join(", \r\n", campos))
            });

            return lista;

        }

        public static List<CodigoLinha> ListaEditarJs(Type model)
        {

            var lista = new List<CodigoLinha>();
            var lowerName = model.Name.ToLower();
            var upperName = model.Name.ToUpper();

            var filtros = new List<string>();
            var retorno = new List<string>();
            var campos = new List<string>();

            foreach (var campo in model.GetProperties())
            {
                switch (Type.GetTypeCode(campo.PropertyType))
                {
                    case TypeCode.DateTime:
                        campos.Add(@" $(""#[campoNome]"").val(DataJson2([lowerName].[campoNome]));".Replace("[campoNome]", campo.Name)
                            .Replace("[lowerName]", lowerName));
                        break;
                    default:
                        campos.Add(@" $(""#[campoNome]"").val([lowerName].[campoNome]);".Replace("[campoNome]", campo.Name)
                            .Replace("[lowerName]", lowerName));
                        break;
                }
            }


            var carregar = @"

        /// - Copiar para o arquivo [lowerName].js e inserir dentro do objeto [lowerName]Controle
        /// Inicio do metodo Gravar[model]


    Carregar[model]: function (id) {
        $.getJSON(urlAjax.[lowerName].Buscar[model], { id }, function ([lowerName]) {
            [campos]
        });
    },

        /// Fim do metodo Gravar[model] -------------------------------------------------


";

            lista.Add(new CodigoLinha()
            {
                Descricao = carregar.Replace("[model]", model.Name)
                        .Replace("[lowerName]", lowerName)
                        .Replace("[upperName]", upperName)
                        .Replace("[campos]", string.Join("\r\n", campos))
            });

            return lista;

        }

        public static List<CodigoLinha> ListaController(Type model)
        {
            var lista = new List<CodigoLinha>();
            var lowerName = model.Name.ToLower();
            var upperName = model.Name.ToUpper();


            var controller = @"

        /// - Copiar para o arquivo [model]Controller.cs
        /// Inicio da classe [model]Controller


    [Filtro(Roles = ""[model]"")]
    public class [model]Controller : Controller
        {
            readonly I[model]Aplicacao [model]Aplicacao;

            public [model]Controller(I[model]Aplicacao [lowerName]Aplicacao)
            {
                [model]Aplicacao = [lowerName]Aplicacao;
            }
            // GET: [model]
            public ActionResult Index()
            {
                return View();
            }

            public ActionResult Novo()
            {
                return View();
            }

            public ActionResult Editar(int id)
            {
                ViewBag.[model]Id = id;
                return View();
            }

            public ActionResult Lista()
            {
                return View();
            }

            public JsonResult Gravar[model]([model] [lowerName])
            {
                try
                {
                    if ([lowerName].[model]Id > 0)
                    {
                        [model]Aplicacao.Atualiza([lowerName]);
                    }
                    else
                    {
                        [model]Aplicacao.Adiciona([lowerName]);
                    }
                    return Json(new { sucesso = true }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { sucesso = false, mensagem = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }

            public JsonResult Listar[model]()
            {
                return Json([model]Aplicacao.BuscaTodos(), JsonRequestBehavior.AllowGet);
            }


            public JsonResult Buscar[model](int id)
            {
                var retorno = [model]Aplicacao.BuscaId(id);
                return Json(retorno, JsonRequestBehavior.AllowGet);
            }

            public JsonResult Deletar[model](int id)
            {
                bool sucesso;
                string mensagem;
                try
                {
                    [model]Aplicacao.Remove(id);
                    sucesso = true;
                    mensagem = ""[model] Deletado Com Sucesso!"";
                }
                catch (Exception e)
                {
                    sucesso = false;
                    mensagem = e.Message;
                }

                return Json(new { sucesso, mensagem }, JsonRequestBehavior.AllowGet);
            }
        }

        /// Fim da classe [model]Controller ---------------------------------------------------------

";

            lista.Add(new CodigoLinha()
            {
                Descricao = controller.Replace("[model]", model.Name)
            .Replace("[lowerName]", lowerName)
            .Replace("[upperName]", upperName)            
            });

            return lista;

        }

        public static List<CodigoLinha> ListaViewLista(Type model)
        {
            var lista = new List<CodigoLinha>();
            var lowerName = model.Name.ToLower();
            var upperName = model.Name.ToUpper();

            var viewLista = @"

        /// - Copiar para o arquivo [model]\Lista.cshtml
        /// Inicio do html


<div class=""content-wrapper"">
    <div class=""content-header"">
        <div class=""container-fluid"">
            <div class=""row md-2"">
                <div class=""col"">
                    < h1>
                        Lista de [model]s
                    </h1>
                </div>
                <div class=""col"">
                    <ol class=""breadcrumb float-sm-right"">
                        <li class=""breadcrumb-item"">
                            <a href=""#"">Home</a>
                        </li>
                        <li class=""breadcrumb-item"">@Html.ActionLink(""[model]"", ""Index"")</li>
                        <li class=""breadcrumb-item active"">Listagem</li>
                    </ol>
                </div>
            </div>
        </div>
    </div>
    <section class=""content"">
       <div class=""container-fluid"">
             <div class=""row"">
                <div class=""col"">
                    <div class=""card"">
                        <div class=""card-body"">
                            <div id=""lista[model]""></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</div>


@section scripts {
    <script>
        $(document).ready(function () {
            [lowerName]Controle.Listagem[model]();
        });
    </script>
}

        /// Fim do html -------------------------------------------------------------------


";

            lista.Add(new CodigoLinha()
            {
                Descricao = viewLista.Replace("[model]", model.Name)
            .Replace("[lowerName]", lowerName)
            .Replace("[upperName]", upperName)
            });

            return lista;

        }

        public static List<CodigoLinha> ListaViewIndex(Type model)
        {
            var lista = new List<CodigoLinha>();
            var lowerName = model.Name.ToLower();
            var upperName = model.Name.ToUpper();

            var viewIndex = @"

        /// - Copiar para o arquivo [model]\Index.cshtml
        /// Inicio do html



 <div class=""content-wrapper"">
    <div class=""content-header"">
        <div class=""container-fluid"">
                <div class=""row md-2"">
                <div class=""col"">
                    <h1>
                    Pagina Inicial
                </h1>
                </div>
                <div class=""col"">
                    <ol class=""breadcrumb float-sm-right"">
                        <li class=""breadcrumb-item"">
                            @Html.ActionLink(""HOME"", ""Index"", ""Home"")
                        </li>
                        <li class=""breadcrumb-item active"">[model]</li>
                    </ol>
                </div>
            </div>
        </div>
    </div>
    <section class=""content"">
        <div class=""container-fluid"">
            <div class=""row"">
                <div class=""col"">
                    <div class=""small-box bg-info"">
                        <div class=""inner"">
                            <h3>Lista</h3>
                            <p>Listagem do cadastro de [lowerName]s</p>
                        </div>
                        <div class=""icon"">
                            <i class=""ion ion-ios-list""></i>
                        </div>
                        <a href=""@Url.Action(""Lista"")"" class=""small-box-footer"">Clique <i class=""fa fa-arrow-circle-right""></i></a>
                    </div>
                </div>
                <div class=""col"">
                    <div class=""small-box bg-green"">
                        <div class=""inner"">
                            <h3>Novo</h3>
                            <p>Cadastrar novo [lowerName]</p>
                        </div>
                        <div class=""icon"">
                            <i class=""ion ion-ios-personadd""></i>
                        </div>
                        <a href=""@Url.Action(""Novo"")"" class=""small-box-footer"">Clique aqui <i class=""fa fa-arrow-circle-right""></i></a>
                    </div>
                </div>
            </div>
        </div>
    </section>
</div>

        /// Fim do html ------------------------------------


";

            lista.Add(new CodigoLinha()
            {
                Descricao = viewIndex.Replace("[model]", model.Name)
            .Replace("[lowerName]", lowerName)
            .Replace("[upperName]", upperName)
            });

            return lista;

        }

        public static List<CodigoLinha> ListaViewNovo(Type model)
        {

            var lista = new List<CodigoLinha>();
            var lowerName = model.Name.ToLower();
            var upperName = model.Name.ToUpper();

            var campos = new List<string>();

            foreach (var campo in model.GetProperties())
            {
                campos.Add(@"<div class=""form-row"">");
                campos.Add(@"   <div class=""col-md-12"">");
                campos.Add($"<label>{campo.Name}</label>");
                switch (Type.GetTypeCode(campo.PropertyType))
                {
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                    case TypeCode.Double:
                    case TypeCode.Decimal:
                        campos.Add($@"<input id=""{campo.Name}"" class=""form-control"" type=""number"" />");
                        break;
                    case TypeCode.DateTime:
                        campos.Add($@"<input id=""{campo.Name}"" class=""form-control"" type=""date"" />");
                        break;
                    default:
                        campos.Add($@"<input id=""{campo.Name}"" class=""form-control"" type=""text"" />");
                        break;
                }
                campos.Add("    </div>");
                campos.Add("</div>");
            }


            var carregar = @"

        /// - Copiar para o arquivo [model]\Novo.cshtml
        /// Inicio do html


        <div class=""content-wrapper"">
            <div class=""content-header"">
                 <div class=""container-fluid"">
                      <div class=""row md-2"">
                         <div class=""col"">
                             <h1>Inserir novo [lowerName]</h1>
                         </div>
                         <div class=""col"">
                            <ol class=""breadcrumb float-sm-right"">
                                <li class=""breadcrumb-item"">@Html.ActionLink(""Home"", ""Index"", ""Home"")</li>
                                <li class=""breadcrumb-item"">@Html.ActionLink(""[model]"", ""Index"")</li>
                                <li class=""breadcrumb-item active"">Novo</li>
                            </ol>
                         </div>
                      </div>
                 </div>
            </div>
            <section class=""content"">
                <div class=""container-fluid""> 
                    <div class=""row"">
                        <div class=""col"">
                            <div class=""card card-primary"">
                                <div class=""card-header"">
                                    <h3 class=""card-title"">Novo [lowerName]</h3>
                                </div>
                                <div class=""card-body"">
                                    <input type=""hidden"" id=""[model]Id"" value=""0"" />
                                    <div class=""form-group"">
                                        [campos]
                                    </div>
                                </div>
                                <div class=""card-footer"">
                                    <button class=""btn btn-primary btn-block"" onclick=""[lowerName]Controle.Gravar[model]();"">Gravar</button>
                                </div>
                            </div>
                        </div>
                    </div>
                 </div> 
            </section>
        </div>

        /// Fim do html ----------------------------------------------------


";

            lista.Add(new CodigoLinha()
            {
                Descricao = carregar.Replace("[model]", model.Name)
                        .Replace("[lowerName]", lowerName)
                        .Replace("[upperName]", upperName)
                        .Replace("[campos]", string.Join("\r\n", campos))
            });

            return lista;

        }

        public static List<CodigoLinha> ListaViewEditar(Type model)
        {

            var lista = new List<CodigoLinha>();
            var lowerName = model.Name.ToLower();
            var upperName = model.Name.ToUpper();

            var campos = new List<string>();

            foreach (var campo in model.GetProperties())
            {
                campos.Add(@"<div class=""form-row"">");
                campos.Add(@"   <div class=""col-md-12"">");
                campos.Add($"<label>{campo.Name}</label>");
                switch (Type.GetTypeCode(campo.PropertyType))
                {
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                    case TypeCode.Double:
                    case TypeCode.Decimal:
                        campos.Add($@"<input id=""{campo.Name}"" class=""form-control"" type=""number"" />");
                        break;
                    case TypeCode.DateTime:
                        campos.Add($@"<input id=""{campo.Name}"" class=""form-control"" type=""date"" />");
                        break;
                    default:
                        campos.Add($@"<input id=""{campo.Name}"" class=""form-control"" type=""text"" />");
                        break;
                }
                campos.Add("    </div>");
                campos.Add("</div>");
            }


            var carregar = @"

        /// - Copiar para o arquivo [model]\Editar.cshtml
        /// Inicio do html


<div class=""content-wrapper"">
    <div class=""content-header"">
        <div class=""container-fluid"">
            <div class=""row md-2"">
               <div class=""col"">
                   <h1>Editar [lowerName]</h1>
               </div> 
               <div class=""col"">
                    <ol class=""breadcrumb float-sm-right"">
                        <li class=""breadcrumb-item"">@Html.ActionLink(""Home"", ""Index"", ""Home"")</li>
                        <li class=""breadcrumb-item"">@Html.ActionLink(""[model]"", ""Index"")</li>
                        <li class=""breadcrumb-item active"">Editar</li>
                    </ol>
               </div> 
            </div>
        </div>
    </div>
    <div class=""content"">
      <div class=""container-fluid"">
        <div class=""row"">
            <div class=""col"">
                <div class=""card card-primary"">
                    <div class=""card-body"">
                        <input type=""hidden"" id=""[model]Id"" value=""@ViewBag.[model]Id"" />
                        <div class=""form-group"">
                            [campos]
                        </div>
                    </div>
                    <div class=""card-footer"">
                        <button class=""btn btn-primary btn-block"" onclick=""[lowerName]Controle.Gravar[model]();"">Gravar</button>
                    </div>
                </div>
            </div>
        </div>
      </div>
    </div>
</div>


@section scripts {
    <script>
            $(document).ready(function () {
                [lowerName]Controle.Carregar[model](@ViewBag.[model]Id);
            });
    </script>
}


        /// Fim do html ---------------------------------------------------------------


";

            lista.Add(new CodigoLinha()
            {
                Descricao = carregar.Replace("[model]", model.Name)
                        .Replace("[lowerName]", lowerName)
                        .Replace("[upperName]", upperName)
                        .Replace("[campos]", string.Join("\r\n", campos))
            });

            return lista;

        }


    }
}