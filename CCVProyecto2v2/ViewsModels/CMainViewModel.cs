﻿using CCVProyecto2v2.DataAccess;
using CCVProyecto2v2.Dto;
using CCVProyecto2v2.Utilidades;
using CCVProyecto2v2.ViewsAdmin;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace CCVProyecto2v2.ViewsModels
{
    public partial class CMainViewModel : ObservableObject
    {
        private readonly DbbContext _dbContext;


        [ObservableProperty]
        private ObservableCollection<ClaseDto> listaClases = new ObservableCollection<ClaseDto>();

        public CMainViewModel(DbbContext context)
        {
            _dbContext = context;

            MainThread.BeginInvokeOnMainThread(new Action(async () => await ObtenerClases()));

            WeakReferenceMessenger.Default.Register<Mensajeria>(this, (r, m) =>
            {
                ClaseMensajeRecibido(m.Value);
            });
        }

        public async Task ObtenerClases()
        {
            var lista = await _dbContext.Clase.Include(c=>c.Profesor).ToListAsync();
            //ListaClases.Clear();

            if (lista.Any())
            {
                foreach (var clase in lista)
                {
                    ListaClases.Add(new ClaseDto
                    {
                        Id = clase.Id,
                        ProfesorId = clase.ProfesorId,
                        Nombre=clase.Nombre,
                        
                        Profesor = new ProfesorDto
                        {
                            Id = clase.Profesor.Id,
                            Nombre = clase.Profesor.Nombre,
                            Cedula = clase.Profesor.Cedula,
                            Edad = clase.Profesor.Edad,
                            Materia = clase.Profesor.Materia
                        }
                    });
                }
            }
        }
        private void ClaseMensajeRecibido(Cuerpo claseCuerpo)
        {
            var claseDto = claseCuerpo.ClaseDto;

            if (claseCuerpo.EsCrear)
            {
                ListaClases.Add(claseDto);
            }
            else
            {
                var encontrada = ListaClases.First(c => c.Id == claseDto.Id);

                encontrada.ProfesorId = claseDto.ProfesorId;
                encontrada.Profesor = claseDto.Profesor;
            }
        }

        [RelayCommand]
        private async Task Crear()
        {
            var uri = $"{nameof(AgregarClaseView)}?id=0";
            await Shell.Current.GoToAsync(uri);
        }

        [RelayCommand]
        private async Task Editar(ClaseDto claseDto)
        {
            var uri = $"{nameof(AgregarClaseView)}?id={claseDto.Id}";
            await Shell.Current.GoToAsync(uri);
        }

        [RelayCommand]
        private async Task Eliminar(ClaseDto claseDto)
        {
            bool anwser = await Shell.Current.DisplayAlert("Mensaje", "¿Desea eliminar esta clase?", "Sí", "No");
            if (anwser)
            {
                var encontrada = await _dbContext.Clase
                    .FirstAsync(c => c.Id == claseDto.Id);

                _dbContext.Clase.Remove(encontrada);
                await _dbContext.SaveChangesAsync();

                ListaClases.Remove(claseDto);
            }
        }
    }
}
