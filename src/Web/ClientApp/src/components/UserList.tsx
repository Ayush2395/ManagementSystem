import React, { useEffect, useState } from "react"
import { UserClient, UserDto } from "../web-api-client.ts"
import Table from "./Table.tsx"
import { User } from "lucide-react"
import { Link } from "react-router-dom"

const list: Record<string, string> = {
    id: "User Id",
    firstName: "First Name",
    lastName: "Last Name",
    created: "Created At",
    lastModified: "Updated on"
}

const UserList = () => {
    const [users, setUsers] = useState<UserDto[]>([])

    useEffect(() => {
        const handleFetchUsers = async () => {
            const client = new UserClient()
            try {
                const response = await client.getUsers()
                if (response.length > 0) {
                    setUsers(response)
                }
            } catch (error) {
                if (error instanceof TypeError) {
                    console.log(error.message);
                } else if (error instanceof ReferenceError) {
                    console.log(error.message);
                } else {
                    console.log(error);
                }
            }
        }
        handleFetchUsers()
        return () => {
            setUsers([])
        }
    }, [])
    return (
        <>
            <Link to="/add-user" type="button" className="btn btn-primary btn-sm">Add users <User strokeWidth={1.25} size={20} /></Link>
            <Table list={list} users={users} />
        </>
    )

}

export default UserList