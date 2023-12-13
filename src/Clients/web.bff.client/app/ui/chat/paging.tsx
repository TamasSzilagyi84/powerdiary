"use client";

import { useSearchParams, usePathname, useRouter } from 'next/navigation';

export default function Paging ({ value }: { value: Number }) {
    const searchParams = useSearchParams();
    const pathname = usePathname();
    const { replace } = useRouter();
    const currentPage: any = value;

    function updateTake(eventValue: Number) {
        const params = new URLSearchParams(searchParams);

        params.set('page', String(eventValue));

        console.log(value);
        replace(`${pathname}?${params.toString()}`);
      }

    return (
        <div className='grid grid-cols-6 mb-6'>
            <div>CurrentPage is: {currentPage}</div>
            <button 
            disabled={currentPage === 1}
            className='rounded-md bg-indigo-600 px-3 py-2 text-sm font-semibold text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600 mr-2' onClick={(e) => {updateTake(currentPage - 1)}}>Previous page</button>
            <button className='rounded-md bg-indigo-600 px-3 py-2 text-sm font-semibold text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600' onClick={(e) => {updateTake(currentPage + 1)}}>Next page</button>
        </div>        
    );
};

  