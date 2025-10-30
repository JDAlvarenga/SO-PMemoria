# Algoritmo Óptimo de Paginación de Memoria

Este repositorio contiene la implementación del **algoritmo óptimo de paginación de memoria**. El algoritmo simula la gestión de memoria en un sistema operativo, mostrando cómo se realiza la asignación de páginas a la memoria y reemplazo de páginas de manera ideal.

Se utiliza [Blazor](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor) compilado a [WASM](https://webassembly.org/), junto con la librería de componentes [MudBlazor](https://mudblazor.com).

Este proyecto está disponible a través del siguiente enlace: [Algoritmo Óptimo de Paginación ](https://jdalvarenga.github.io/SO-PMemoria/)

## Descripción

El algoritmo óptimo de paginación de memoria es una técnica que optimiza el reemplazo de páginas en un sistema con memoria virtual. Este algoritmo selecciona la página que no será utilizada por más tiempo en el futuro y la reemplaza cuando sea necesario. A pesar de ser teóricamente el más eficiente, su implementación en un sistema real no es posible a la necesidad de conocer las futuras referencias de memoria.

Este proyecto permite visualizar el funcionamiento de este algoritmo **paso a paso**.

## Características

- **Simulación paso a paso**: Permite al usuario visualizar cómo se gestionan las páginas de memoria en cada paso de ejecución.
- **Interfaz intuitiva**: La visualización es clara y fácil de seguir, lo que facilita el análisis.
- **Configuración flexible**: Se pueden modificar los parámetros iniciales como el número de marcos de memoria y la secuencia de referencias de páginas.
- **Resumen de resultados**: A medida que se avanza en la simulación se registran y contabilizan eventos relevantes durante el proceso como:
  - Solicitudes de páginas
  - Fallos de página
  - Carga de las páginas en memoria
  - Liberación de marcos de página

## Explicación del Algoritmo

El algoritmo óptimo selecciona la página que no será utilizada por más tiempo en el futuro. Si una página ya está en memoria y se vuelve a referenciar, no se hace ningún cambio. Si una página no está en memoria, se carga en uno de los marcos de memoria disponibles.

Para el control de las asignaciones se mantiene:
- Una lista de las páginas en el orden que serán solicitadas
- Un diccionario de las páginas que se encuentran cargadas en memoria, junto con el marco de página en el que se encuentra.
- Un diccionario de todas las páginas. Este contiene todas las ocurrencias de la página en la lista (su índice).
- Una cola de prioridad (max heap) en donde se mantienen las páginas alojadas en memoria, ordenadas de acuerdo al índice de la lista más próximo en un momento dado.

Esto se hace para lograr asignaciones de memoria en el menor tiempo posible. 


