import { Suspense } from "react";
import { fetchChatMessages } from "@/app/lib/data";
import { lusitana } from '@/app/ui/fonts';
import { convertDateTimeString } from "@/app/lib/utils";

interface ChatMessage {
    id: number;
    type: string;
    message: string;
    created: string;
}

export default async function Page({ inputTake, inputPage }: { inputTake?: Number, inputPage?: Number }) {
    const take = Number(inputTake) || 20;
    let page = Number(inputPage) || 1;
    page -= 1;

    const chatMessages: ChatMessage[] = await fetchChatMessages(page, take);

    return (
        <div>
            <Suspense fallback={<div>Loading...</div>}>
                <div className="w-full md:col-span-4">
                    <h2 className={`${lusitana.className} mb-4 text-xl md:text-2xl`}>
                        Messages
                    </h2>
                    {chatMessages.map((chatMessage: ChatMessage) => (
                            <div key={chatMessage.id} className="flex flex-col gap-2 text-sm">
                                <div className="w-full rounded-md bg-blue-300 p-1 mb-2">
                                    {convertDateTimeString(chatMessage.created)}: {chatMessage.message}
                                </div>
                            </div>
                        ))}
                </div>
            </Suspense>
        </div>
    );
}
