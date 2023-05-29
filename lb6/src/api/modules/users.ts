import apiClient from "../client";

export const getById = (id: string) => apiClient({
  path: `users/${id}`,
  method: 'GET'
})

export const getByPage = (page: number) => apiClient({
  path: `users?page=${page}`,
  method: 'GET'
})

export const addUser = ({ name, job }: { name: string, job: string }) => apiClient({
  path: `users`,
  method: 'post',
  data: { name, job }
})

export const updateUser = ({id, first_name,second_name, img, email }: 
  {id: string, first_name: string, second_name: string, img: string, email: string}) => apiClient({
  path: `users/${id}`,
  method: 'put',
  data: {first_name, second_name, img, email}
})
