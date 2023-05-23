import apiClient from "../client";

export const getById = (id: string) => apiClient({
  path: `resources/${id}`,
  method: 'GET'
})

export const getByPage = (page: number) => apiClient({
  path: `resources?page=${page}`,
  method: 'GET'
})

export const updateResource = ({id, name, color, year, pantone_value }: 
  {id:string, name: string, color: string, year:string, pantone_value:string }) => apiClient({

  path: `resource/${id}`,
  method: 'put',
  data: {name, color, year, pantone_value }
})